using AventStack.ExtentReports;
using Common;
using Common.StepDefinitions;
using Common.Utility;
using EnCompass.Testing.Source.Utility;
using NFluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace EnCompass.Testing.Source.StepDefinitions.API
{
    [Binding]
    public sealed class SoapApiSteps : StepDefinition
    {

        public SoapApiSteps(GlobalSettings settings) : base(settings) { }

		#region Modify File For API

		// Usually in PROD we SKIP Account, Merchant creation steps, BUT only for -ve cases we need NOT skip.
		[Given(@"I modify the file ""(.*)"" with preset data ""(.*)"" to test negative cases in PROD and set")]
		public void GivenIModifyTheFileWithPresetDataToTestNegativeCasesInPRODAndSet(string fileName, string dataSet, Table fieldsToModify)
		{
			ExtentTest test = Settings.EnCompassExtentTest;
			Tuple<string, string> _envFi = new Tuple<string, string>(GlobalSettings.Environment.ToLower(), GlobalSettings.FI);
			ModifyAPITemplateFromDataTable(fileName, dataSet, fieldsToModify, test, _envFi);
		}


		[Given(@"I modify the file ""(.*)"" with preset data ""(.*)"" and set")]
        public void GivenIModifyTheFileWithPresetDataAndSet(string fileName, string dataSet, Table fieldsToModify)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
			List<string> reqToSkip = new List<string> { "TestAccounts_Request", "CreateMerchant_Request","CreateMerchant(APTraditional)_Request" };
			Tuple<string, string> _envFi = new Tuple<string, string>(GlobalSettings.Environment.ToLower(), GlobalSettings.FI);

			// Skip Account and Merchant Creation if its PROD
			if (GlobalSettings.Environment.Equals("prod") && reqToSkip.Any(a => fileName.Contains(a)))
			{
				// Set the Scenario Ctx For FileAbsPath to null
				Settings.Scenario["FileAbsPath"] = null;

				// For Non-SUGA accounts we need the Account Number. 
				StringKeys.AuthAccountNumbers.TryGetValue(_envFi, out string accNum);
				Settings.Scenario["AccountNumber"] = accNum;

				// For Get Account Data API we need SUGA acc numbers
				StringKeys.ProdSUGAAccountsAPI.TryGetValue(GlobalSettings.FI, out string sugaAcc);
				Settings.Scenario["AccountNumbers/string"] = sugaAcc;

				// Store the Merchant Codes, since we're skipping Merchant Creation
				StringKeys.ProdMerchantCodesAPI.TryGetValue(GlobalSettings.FI, out string mcode);
				Settings.Scenario["MerchantCode"] = mcode;

				test.Skip("Skipping The Step as Env = PROD and Request is :" + fileName);
			}
			else
			{
				ModifyAPITemplateFromDataTable(fileName, dataSet, fieldsToModify, test, _envFi);
			}
		}

        [Given(@"I modify the file at ""(.*)"" to change field value for ""(.*)"" to ""(.*)""")]
        public void GivenIModifyTheFileToChangeFieldValueForTo(SourceString fileAbsPath, SourceString fieldName, SourceString value)
        {
            ExtentTest test = Settings.EnCompassExtentTest;

			// fileAbsPath set to Empty in earlier steps if its runnin in PROD for unwanted APIs
			if (String.IsNullOrEmpty(fileAbsPath))
			{
				test.Skip("Skipping The Step as Env = PROD and Request is :" + fileAbsPath);
			}
			else
			{
				try
				{
					test.Info("Absolute path of file to be modified: " + fileAbsPath);

					// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
					XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
					// Get XDocument for the file
					XDocument soapXmlReq = XDocument.Load(fileAbsPath);

					// Call Global Transforms to process the Value first
					string fieldValue = (value.ToString().ToLowerInvariant().Contains("none")) ? "" :
										 new GlobalTransforms(Settings).LookupInExternalDatasource(value).ToString();

					// If input string contains the pattern {0} then replace all such patterns with unique values
					// in the Field Name. The same will be searched in the xml
					Regex regex = new Regex(@"[{][0-9][}]");
					foreach (Match match in regex.Matches(fieldValue))
					{
						fieldValue = string.Format(fieldValue, DateTime.UtcNow.ToString("mmssffff"));

						Settings.Scenario["ZeroPattern"] = fieldValue;
					}

					// Replace 'today/tomorrow' with apt dates
					if (fieldValue.ToLower().Equals("today") || fieldValue.ToLower().Equals("tomorrow") || fieldValue.ToLower().Equals("yesterday"))
						fieldValue = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(DateHelper.GetDateValueBasedOnInput(fieldValue)));

					// If field name has multiple hierarchies then we evaluate them
					// For e.g  <Invoice>  <description> .. </description> </invoice> will be used as 
					// Invoice/description to uniquely identify it.  Since description field is present for other items
					if (fieldName.ToString().Contains("/"))
					{
						// If we have a hierarchy to maintain then we need to split the hierarchy to navigate to the node
						List<string> nodes = fieldName.ToString().Split('/').ToList();
						IEnumerable<XElement> rootDescendent = null;
						// Iterate all the descendent nodes drilling down to the last node and only then setting the value.
						for (int i = 0; i < nodes.Count; i++)
						{
							rootDescendent = (rootDescendent == null) ? soapXmlReq.Root.Descendants(soap + nodes.ElementAt(i)) : rootDescendent.Descendants(soap + nodes.ElementAt(i));
							if (i == nodes.Count - 1) // i.e for final node we set the value
								rootDescendent.FirstOrDefault().Value = fieldValue;
						}
					}
					else
					{
						// enter hard coded values
						soapXmlReq.Root.Descendants(soap + fieldName).FirstOrDefault().Value = fieldValue;
					}

					test.Debug(fieldName.ToString() + " set to : " + fieldValue);
					soapXmlReq.Save(fileAbsPath);
					test.Info("Saved file after modifying values");

					// Store field modified in the file in scenario context
					Settings.Scenario[fieldName] = fieldValue;
					test.Info("Stored Field Value in Scenario Context named: " + fieldName + " with value :" +
									Settings.Scenario[fieldName].ToString());
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
        }

        [When(@"I modify the file ""(.*)"" with username=""(.*)"", password=""(.*)"",PaymentMethod=""(.*)"",NotificationMethod=""(.*)"",BankNumber=""(.*)"",CompanyNumber=""(.*)"",MerchantCode=""(.*)"",UseSingleUseAccounts=""(.*)"",SingleUseAccountViewPIN=""(.*)"",SingleUseAccountViewPINRequired=""(.*)"",EmailPinNotice=""(.*)""")]
        public void WhenIModifyTheFileWithUsernamePasswordPaymentMethodNotificationMethodBankNumberCompanyNumberMerchantCodeUseSingleUseAccountsSingleUseAccountViewPINSingleUseAccountViewPINRequiredEmailPinNotice(string fileName, SourceString uname, SourceString pwd, string pmntMethod, string notfMethod, SourceString bankNo, SourceString compNo, string mcode, string useSuga, string sugaPin, string sugaPinRqd, string emailPin)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            try
            {
                string fileAbsPath = FileUploadHelper.GetRenamedFileForAPIRequest(Settings, fileName);
                test.Info("Absolute path of renamed file to be modified: " + fileAbsPath);

                // add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
                XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
                // Get XDocument for the file
                XDocument soapXmlReq = XDocument.Load(fileAbsPath);

                // username and pwd are mandatory values
                soapXmlReq.Root.Descendants(soap + "Username").FirstOrDefault().Value = uname.ToString();
                soapXmlReq.Root.Descendants(soap + "Password").FirstOrDefault().Value = pwd.ToString();

                // modify values ONLY if specified with anything other than 'none' or blank string
                if (!(String.IsNullOrWhiteSpace(pmntMethod) || pmntMethod.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "PaymentMethod").ToList().ForEach(d => d.Value = pmntMethod); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(bankNo.ToString()) || bankNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "BankNumber").FirstOrDefault().Value = bankNo.ToString();

                if (!(String.IsNullOrWhiteSpace(mcode) || mcode.ToLower().Trim().Equals("none")))
                {
                    soapXmlReq.Root.Descendants(soap + "MerchantCode").FirstOrDefault().Value = mcode;
                    soapXmlReq.Root.Descendants(soap + "Name").FirstOrDefault().Value = mcode;
                }

                if (!(String.IsNullOrWhiteSpace(compNo.ToString()) || compNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "CompanyNumber").FirstOrDefault().Value = compNo.ToString();

                if (!(String.IsNullOrWhiteSpace(notfMethod) || notfMethod.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "NotificationMethod").ToList().ForEach(d => d.Value = notfMethod); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(useSuga) || useSuga.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "UseSingleUseAccounts").ToList().ForEach(d => d.Value = useSuga); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(sugaPin) || sugaPin.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "SingleUseAccountViewPIN").ToList().ForEach(d => d.Value = sugaPin); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(sugaPinRqd) || sugaPinRqd.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "SingleUseAccountViewPINRequired").ToList().ForEach(d => d.Value = sugaPinRqd); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(emailPin) || emailPin.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "EmailPinNotice").FirstOrDefault().Value = emailPin;
                else
                    soapXmlReq.Root.Descendants(soap + "EmailPinNotice").Remove();

                soapXmlReq.Save(fileAbsPath);
                test.Info("Saved file after modifying values");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [When(@"I modify the file ""(.*)"" with username=""(.*)"", password=""(.*)"",PaymentMethod=""(.*)"",NotificationMethod=""(.*)"",BankNumber=""(.*)"",CompanyNumber=""(.*)"",MerchantCode=""(.*)"",ACHRoutingNumber=""(.*)"",ACHAccountNumber=""(.*)"",AchFileDetailRecordFormat =""(.*)""")]
        public void WhenIModifyTheFileWithUsernamePasswordPaymentMethodNotificationMethodBankNumberCompanyNumberMerchantCodeACHRoutingNumberACHAccountNumberAchFileDetailRecordFormat(string fileName, SourceString uname, SourceString pwd, string pmntMethod, string notfMethod, SourceString bankNo, SourceString compNo, string mcode, string achRoutingNo, string achAccNo, string achFileFormat)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            try
            {
                string fileAbsPath = FileUploadHelper.GetRenamedFileForAPIRequest(Settings, fileName);
                test.Info("Absolute path of renamed file to be modified: " + fileAbsPath);
                // add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
                XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
                // Get XDocument for the file
                XDocument soapXmlReq = XDocument.Load(fileAbsPath);

                // username and pwd are mandatory values
                soapXmlReq.Root.Descendants(soap + "Username").FirstOrDefault().Value = uname.ToString();
                soapXmlReq.Root.Descendants(soap + "Password").FirstOrDefault().Value = pwd.ToString();

                // modify values ONLY if specified with anything other than 'none' or blank string
                if (!(String.IsNullOrWhiteSpace(pmntMethod) || pmntMethod.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "PaymentMethod").ToList().ForEach(d => d.Value = pmntMethod); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(bankNo.ToString()) || bankNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "BankNumber").FirstOrDefault().Value = bankNo.ToString();

                if (!(String.IsNullOrWhiteSpace(mcode) || mcode.ToString().ToLower().Trim().Equals("none")))
                {
                    soapXmlReq.Root.Descendants(soap + "MerchantCode").FirstOrDefault().Value = mcode;
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "Name").FirstOrDefault().Value = mcode;
                }

                if (!(String.IsNullOrWhiteSpace(compNo.ToString()) || compNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "CompanyNumber").FirstOrDefault().Value = compNo.ToString();

                if (!(String.IsNullOrWhiteSpace(notfMethod) || notfMethod.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "NotificationMethod").ToList().ForEach(d => d.Value = notfMethod); // multiple nodes needs to be set


                if (!(String.IsNullOrWhiteSpace(achRoutingNo) || achRoutingNo.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "ACHRoutingNumber").FirstOrDefault().Value = achRoutingNo;
                    else
                        soapXmlReq.Root.Descendants(soap + "BankRoutingNumber").FirstOrDefault().Value = achRoutingNo;
                }

                if (!(String.IsNullOrWhiteSpace(achAccNo) || achAccNo.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "ACHAccountNumber").FirstOrDefault().Value = achAccNo;
                    else
                        soapXmlReq.Root.Descendants(soap + "BankAccountNumber").FirstOrDefault().Value = achAccNo;
                }

                if (!(String.IsNullOrWhiteSpace(achFileFormat) || achFileFormat.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "AchFileDetailRecordFormat").FirstOrDefault().Value = achFileFormat;
                    else
                        soapXmlReq.Root.Descendants(soap + "AchCreditFormat").FirstOrDefault().Value = achFileFormat;
                }
                else
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "AchFileDetailRecordFormat").Remove();
                }

                soapXmlReq.Save(fileAbsPath);
                test.Info("Saved file after modifying values");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [When(@"I modify the file (.*) with username=""(.*)"", password=""(.*)"",PaymentMethod=(.*),NotificationMethod=(.*),BankNumber=""(.*)"",CompanyNumber=""(.*)"",MerchantCode=(.*),EmailAddress=(.*),ACHRoutingNumber=(.*),ACHAccountNumber=(.*),AchFileDetailRecordFormat=(.*),UseSingleUseAccounts=(.*),SingleUseAccountViewPIN=(.*),SingleUseAccountViewPINRequired=(.*),EmailPinNotice=(.*)")]
        public void WhenIModifyTheFileWithUsernamePasswordPaymentMethodNotificationMethodBankNumberCompanyNumberMerchantCodeACHRoutingNumberACHAccountNumberAchFileDetailRecordFormatUseSingleUseAccountsSingleUseAccountViewPINSingleUseAccountViewPINRequiredEmailPinNotice(string fileName, SourceString uname, SourceString pwd, string pmntMethod, string notfMethod, SourceString bankNo, SourceString compNo, string mcode, string email, string achRoutingNo, string achAccNo, string achFileFormat, string useSuga, string sugaPin, string sugaPinRqd, string emailPin)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            try
            {
                string fileAbsPath = FileUploadHelper.GetRenamedFileForAPIRequest(Settings, fileName);
                test.Info("Absolute path of renamed file to be modified: " + fileAbsPath);
                // add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
                XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
                // Get XDocument for the file
                XDocument soapXmlReq = XDocument.Load(fileAbsPath);

                // username and pwd are mandatory values
                Settings.Scenario["UserName"] = soapXmlReq.Root.Descendants(soap + "Username").FirstOrDefault().Value = uname.ToString();
                soapXmlReq.Root.Descendants(soap + "Password").FirstOrDefault().Value = pwd.ToString();

                if (uname.ToString().ToLower().Contains("programadmin"))
                    soapXmlReq.Root.Descendants(soap + "OrgGroupLoginId").FirstOrDefault().Value = Settings.Scenario["OrganizationID"].ToString();
                else
                    soapXmlReq.Root.Descendants(soap + "OrgGroupLoginId").FirstOrDefault().Value = "encsuper";

                // modify values ONLY if specified with anything other than 'none' or blank string
                if (!(String.IsNullOrWhiteSpace(pmntMethod) || pmntMethod.ToLower().Trim().Equals("none")))
                {
                    Settings.Scenario["PaymentMethod"] = pmntMethod;
                    soapXmlReq.Root.Descendants(soap + "PaymentMethod").ToList().ForEach(d => d.Value = pmntMethod); // multiple nodes needs to be set
                }

                if (!(String.IsNullOrWhiteSpace(bankNo.ToString()) || bankNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "BankNumber").FirstOrDefault().Value = bankNo.ToString();

                if (!(String.IsNullOrWhiteSpace(mcode) || mcode.ToString().ToLower().Trim().Equals("none")))
                {
                    Settings.Scenario["MerchantCode"] = mcode;
                    soapXmlReq.Root.Descendants(soap + "MerchantCode").FirstOrDefault().Value = mcode;
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "Name").FirstOrDefault().Value = mcode;
                }

                if (!(String.IsNullOrWhiteSpace(compNo.ToString()) || compNo.ToString().ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "CompanyNumber").FirstOrDefault().Value = compNo.ToString();

                if (!(String.IsNullOrWhiteSpace(notfMethod) || notfMethod.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "NotificationMethod").ToList().ForEach(d => d.Value = notfMethod); // multiple nodes needs to be set

                try
                {
                    if (!(String.IsNullOrWhiteSpace(email.ToString()) || email.ToString().ToLower().Trim().Equals("none")))
                        soapXmlReq.Root.Descendants(soap + "EmailAddresses").Descendants(soap + "string").FirstOrDefault().Value = email.ToString();
                }
                catch (Exception) { }


                if (!(String.IsNullOrWhiteSpace(achRoutingNo) || achRoutingNo.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "ACHRoutingNumber").FirstOrDefault().Value = achRoutingNo;
                    else
                        soapXmlReq.Root.Descendants(soap + "BankRoutingNumber").FirstOrDefault().Value = achRoutingNo;
                }

                if (!(String.IsNullOrWhiteSpace(achAccNo) || achAccNo.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "ACHAccountNumber").FirstOrDefault().Value = achAccNo;
                    else
                        soapXmlReq.Root.Descendants(soap + "BankAccountNumber").FirstOrDefault().Value = achAccNo;
                }

                if (!(String.IsNullOrWhiteSpace(achFileFormat) || achFileFormat.ToLower().Trim().Equals("none")))
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "AchFileDetailRecordFormat").FirstOrDefault().Value = achFileFormat;
                    else
                        soapXmlReq.Root.Descendants(soap + "AchCreditFormat").FirstOrDefault().Value = achFileFormat;
                }
                else
                {
                    if (!fileName.ToLower().Contains("log"))
                        soapXmlReq.Root.Descendants(soap + "AchFileDetailRecordFormat").Remove();
                    else
                        soapXmlReq.Root.Descendants(soap + "AchCreditFormat").Remove();
                }

                if (!(String.IsNullOrWhiteSpace(useSuga) || useSuga.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "UseSingleUseAccounts").ToList().ForEach(d => d.Value = useSuga); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(sugaPin) || sugaPin.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "SingleUseAccountViewPIN").ToList().ForEach(d => d.Value = sugaPin); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(sugaPinRqd) || sugaPinRqd.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "SingleUseAccountViewPINRequired").ToList().ForEach(d => d.Value = sugaPinRqd); // multiple nodes needs to be set

                if (!(String.IsNullOrWhiteSpace(emailPin) || emailPin.ToLower().Trim().Equals("none")))
                    soapXmlReq.Root.Descendants(soap + "EmailPinNotice").FirstOrDefault().Value = emailPin;
                else
                    if (soapXmlReq.Root.Descendants(soap + "EmailPinNotice").Any())
                    soapXmlReq.Root.Descendants(soap + "EmailPinNotice").Remove();

                soapXmlReq.Save(fileAbsPath);
                test.Info("Saved file after modifying values");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Invoke Webservice

        [When(@"I invoke the SOAP WebService ""(.*)"" with Method= ""(.*)"", Body=""(.*)""")]
        [Then(@"I invoke the SOAP WebService ""(.*)"" with Method= ""(.*)"", Body=""(.*)""")]
        public void ThenIInvokeTheSOAPWebServiceWithMethodBody(string uri, string method, SourceString body)
        {
            ExtentTest test = Settings.EnCompassExtentTest;

			// Check if Body (i.e FileAbsPath) is Empty, in Request modification step it would be set to empty string for PROD
			if (String.IsNullOrEmpty(body))
			{
				Settings.Scenario["XmlRequestObject"] = String.Empty;
				Settings.Scenario["XmlResponseObject"] = String.Empty;
				Settings.Scenario["WebResponse"] = String.Empty;
				Settings.Scenario["Body"] = String.Empty;
				test.Skip("Skipping The Step as Env = PROD");
			}
			else
			{
				// Store the input params in scenario context. This is needed because at times
				// the retrieval call fails (as it needs more time to reflect the changes), then 
				// we need to call this step internally from the catch block
				Settings.Scenario["Uri"] = uri;
				Settings.Scenario["Method"] = method;
				Settings.Scenario["Body"] = body;

				try
				{
					// Get XDocument for the file
					XDocument soapXmlReq = XDocument.Load(body.ToString());

					// Get the Http Request object 	https://wexqaservices.encompass-suite.com/services/
					uri = GlobalSettings.BaseEncompassWebSerivceUrl + uri;
					HttpWebRequest request = SOAPClientHelper.CreateWebRequest(uri, method.ToUpperInvariant().Trim());
					test.Info("Created Http Request object for uri :" + uri + " and method : " + method);

					// Submit the Request to call the appropriate Method in the XML passed.
					using (Stream stream = request.GetRequestStream())
					{
						soapXmlReq.Save(stream);
						test.Info("Saved the XML Document to the Http Request Stream");
						Settings.Scenario["XmlRequestObject"] = soapXmlReq;
						PrintXmlRequestPayloadAsDebugLog(soapXmlReq, request);
					}

					WebResponse response = request.GetResponse();
					test.Info("Got Http Response for the request sent");
					Settings.Scenario["WebResponse"] = response;
				}
				catch (Exception ex)
				{
					test.Error("Exception thrown while getting Http Response for the request :" + ex.InnerException);
					throw ex;
				}
			}
        }

        [Then(@"I invoke the SOAP WebService ""(.*)"" with Method= ""(.*)"", Body=""(.*)"" and Headers")]
        public void ThenIInvokeTheSOAPWebServiceWithMethodBodyAndHeaders(string uri, string method, SourceString body)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            try
            {
                // Get XDocument for the file
                XDocument soapXmlReq = XDocument.Load(body.ToString());
                // Get the Http Request object 	https://wexqaservices.encompass-suite.com/services/
                uri = GlobalSettings.BaseEncompassWebSerivceUrl + uri;
                HttpWebRequest request = SOAPClientHelper.CreateWebRequestAisp(uri, method.ToUpperInvariant().Trim(), Settings.Scenario["aispName"].ToString());
                test.Info("Created Http Request object for uri :" + uri + " and method : " + method);

                // Submit the Request to call the appropriate Method in the XML passed.
                using (Stream stream = request.GetRequestStream())
                {
                    soapXmlReq.Save(stream);
                    test.Info("Saved the XML Document to the Http Request Stream");
                    PrintXmlRequestPayloadAsDebugLog(soapXmlReq, request);
                }

                WebResponse response = request.GetResponse();
                test.Info("Got Http Response for the request sent");
                Settings.Scenario["WebResponse"] = response;

            }
            catch (Exception ex)
            {
                test.Error("Exception thrown while getting Http Response for the request :" + ex.Message);
                throw ex;
            }
        }

        #endregion


        #region Store Values from Request/Response

        [Then(@"I store the value of the fields ""(.*)"" from the request in scenario context")]
        public void ThenIStoreTheValueOfTheFieldsFromTheRequestInScenarioContext(List<string> valuesToStore)
        {
            ExtentTest test = Settings.EnCompassExtentTest;

			if (String.IsNullOrEmpty(Settings.Scenario["XmlRequestObject"].ToString()))
			{
				test.Debug("XmlRequestObject in Scenario Ctx is Null or Empty");
				test.Skip("Skipping The Step as Env = PROD");
			}
			else
			{
				try
				{
					// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
					XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
					XDocument xmlRequest = Settings.Scenario["XmlRequestObject"] as XDocument;
					string scnCtxName = String.Empty;

					// Store xmlRequest values in scenario context
					valuesToStore.ForEach(d =>
					{
						scnCtxName = d;

						if (d.Contains("/"))
						{
							test.Info("Storing value for : " + d + " from Request");
							test.Debug("The Field Name has multiple hierarchis delimitted by '/'");
						// If we have a hierarchy to maintain then we need to split the hierarchy to navigate to the node
						// and retrieve the value. For e.g.  <EmailAddresses> <string> ee@test.com </string> </EmailAddresses>
						List<string> nodes = d.Split('/').ToList();
							IEnumerable<XElement> rootDescendent = null;
							for (int i = 0; i < nodes.Count; i++)
							{
								rootDescendent = (rootDescendent == null) ? xmlRequest.Root.Descendants(soap + nodes.ElementAt(i)) : rootDescendent.Descendants(soap + nodes.ElementAt(i));

								if (i == nodes.Count - 1) // i.e for final node we need the value
							{
									Settings.Scenario[scnCtxName] = rootDescendent.FirstOrDefault().Value;
									test.Info("Stored Field Value in Scenario Context named: " + scnCtxName + " with value :" +
										Settings.Scenario[scnCtxName].ToString());
								}
							}
						}
						else if (!string.IsNullOrWhiteSpace(d))
						{
							test.Info("Storing value for : " + d);
							Settings.Scenario[scnCtxName] = xmlRequest.Root.Descendants(soap + d).FirstOrDefault().Value;
							test.Info("Stored Field Value in Scenario Context named: " + scnCtxName + " with value :" +
										Settings.Scenario[scnCtxName].ToString());
						}
					});
				}
				catch (Exception ex)
				{
					test.Error("Exception thrown while verifying Http Response. " + ex.InnerException);
					throw ex;
				}
			}
        }


        [Then(@"I store the value of the fields ""(.*)"" from the response in scenario context")]
        public void ThenIStoreTheValueOfTheFieldsFromTheResponseInScenarioContext(List<string> valuesToStore)
        {
            ExtentTest test = Settings.EnCompassExtentTest;

			if (String.IsNullOrEmpty(Settings.Scenario["XmlResponseObject"].ToString()))
			{
				test.Debug("XmlResponseObject in Scenario Ctx is Null or Empty");
				test.Skip("Skipping The Step as Env = PROD");
			}
			else
			{
				try
				{
					// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
					XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
					XDocument xmlResponse = Settings.Scenario["XmlResponseObject"] as XDocument;
					string scnCtxName = String.Empty;

					// Store Response values in scenario context
					valuesToStore.ForEach(d =>
					{
						scnCtxName = d;

						if (d.Contains("/"))
						{
							test.Info("Storing value for : " + d + " from Response");
							test.Debug("The Field Name has multiple hierarchis delimitted by '/'");
						// If we have a hierarchy to maintain then we need to split the hierarchy to navigate to the node
						// and retrieve the value. For e.g.  <EmailAddresses> <string> ee@test.com </string> </EmailAddresses>
						List<string> nodes = d.Split('/').ToList();
							IEnumerable<XElement> rootDescendent = null;
							for (int i = 0; i < nodes.Count; i++)
							{
								rootDescendent = (rootDescendent == null) ? xmlResponse.Root.Descendants(soap + nodes.ElementAt(i)) : rootDescendent.Descendants(soap + nodes.ElementAt(i));

								if (i == nodes.Count - 1) // i.e for final node we need the value
							{
									Settings.Scenario[scnCtxName] = rootDescendent.FirstOrDefault().Value;
									test.Info("Stored Field Value in Scenario Context named: " + scnCtxName + " with value: " +
										Settings.Scenario[scnCtxName].ToString());
								}
							}
						}
						else if (!string.IsNullOrWhiteSpace(d))
						{
							test.Info("Storing value for : " + d);
							Settings.Scenario[scnCtxName] = xmlResponse.Root.Descendants(soap + d).FirstOrDefault().Value;
							test.Info("Stored Field Value in Scenario Context named: " + scnCtxName + " with value :" +
							   Settings.Scenario[scnCtxName].ToString());
						}
					});
				}
				catch (Exception ex)
				{
					test.Error("Exception thrown while verifying Http Response. " + ex.InnerException);
					throw ex;
				}
			}
        }

        #endregion


        #region WebService Response Verification

        [Then(@"I verify the response with ResponseCode as ""(.*)"" and message ""(.*)""")]
        [Then(@"I verify the response with ResponseCode =(.*) and message (.*)")]
        public void ThenIVerifyTheResponseWithResponseCodeAndMessage(string respCode, string message)
        {

            ExtentTest test = Settings.EnCompassExtentTest;
            // Process the message such that any global transform substitution to be done are taken care. 
            message = new GlobalTransforms(Settings).LookupInExternalDatasource(message).ToString();
            string resposeCode = null;
            string description = null;

            try
            {
                VerifyAPIRespCodeAndDesc(respCode, message, test, out resposeCode, out description);
            }
            catch (Exception ex)
            {
                // For the first time failure do a retry as often API retrieval call fails and needs time
                System.Threading.Thread.Sleep(5000);
                ThenIInvokeTheSOAPWebServiceWithMethodBody(Settings.Scenario["Uri"].ToString(), Settings.Scenario["Method"].ToString(),
                Settings.Scenario["Body"] as SourceString);
                try
                {
                    test.Warning("Exception thrown while calling the API, retrying with 5 secs wait");
                    VerifyAPIRespCodeAndDesc(respCode, message, test, out resposeCode, out description);
                }
                catch (Exception)
                {
                    // Subsequent Exception needs to be rethrown
                    test.Info("Response Code : " + resposeCode + " expected Response Code :" + respCode);
                    test.Info("Description : " + description + " expected Description :" + message);
                    test.Error("Exception re-thrown while verifying Http Response for the request : " + ex.InnerException);
                    throw ex;
                }
            }
        }
		
        [Then(@"I verify that the ""(.*)"" is present in the response")]
        public void ThenIVerifyThatTheIsPresentInTheResponse(List<string> valuesToVerify)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            string actualNodeValue = null;
            string expNodeValue = null;

            try
            {
                VerifyAPIResponseValues(valuesToVerify, test, out actualNodeValue, out expNodeValue);
            }
            catch (Exception ex)
            {
                try
                {
                    test.Warning("Exception thrown while verifying value in the response, retrying with 5 secs wait");
                    System.Threading.Thread.Sleep(5000);
                    // For the first time failure do a retry as often API retrieval call fails and needs time
                    ThenIVerifyTheResponseWithResponseCodeAndMessage(Settings.Scenario["ExpRespCode"].ToString(), Settings.Scenario["ExpDesc"].ToString());
                    // Verify the Response values
                    VerifyAPIResponseValues(valuesToVerify, test, out actualNodeValue, out expNodeValue);
                }
                catch (Exception)
                {
                    test.Info("Response Actual Value : " + actualNodeValue + " expected Response value : " + expNodeValue);
                    test.Error("Exception thrown while verifying Http Response. " + ex.InnerException);
                    throw ex;
                }
            }
        }

        [Then(@"I go to verify the response with ResponseCode = ""(.*)"", message ""(.*)"", returns the correct OrgGroup ""(.*)"" and IsRevoked is ""(.*)""")]
        public void ThenIGoToVerifyTheResponseWithResponseCodeMessageReturnsTheCorrectOrgGroupAndIsRevokedIs(string respCode, string message, SourceString orgGroupID, string revokedValue)
        {
            ExtentTest test = Settings.EnCompassExtentTest;
            string resposeCode = null;
            string description = null;
            string orgGroupLoginId = null;
            string isRevoked = null;

            try
            {
                // add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
                XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";

                WebResponse response = Settings.Scenario["WebResponse"] as WebResponse;
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XDocument xmlResponse = XDocument.Parse(soapResult);
                    Settings.Scenario["XmlResponseObject"] = xmlResponse;
                    PrintXmlResponsePayloadAsDebugLog(xmlResponse, response);

                    // Save the ResponseCode and Description tag.
                    resposeCode = xmlResponse.Root.Descendants(soap + "ResponseCode").FirstOrDefault().Value;
                    description = xmlResponse.Root.Descendants(soap + "Description").FirstOrDefault().Value;
                    orgGroupLoginId = xmlResponse.Root.Descendants(soap + "OrgGroupLoginId").FirstOrDefault().Value;
                    isRevoked = xmlResponse.Root.Descendants(soap + "IsRevoked").FirstOrDefault().Value;

                    // Verify Response Code
                    Check.That(resposeCode.Trim().ToLowerInvariant()).Equals(respCode.Trim().ToLowerInvariant());
                    test.Info("response code matched expected code :" + respCode);

                    // Verify the Description tag.
                    Check.That(description.Trim().ToLowerInvariant()).Contains(message.Trim().ToLowerInvariant());
                    test.Info("description matched for web service");

                    // Verify the Description tag.
                    Check.That(orgGroupLoginId.Trim()).Contains(orgGroupID.ToString());
                    test.Info("description matched for web service");

                    // Verify the Description tag.
                    Check.That(isRevoked.Trim()).Contains(revokedValue);
                    test.Info("description matched for web service");
                }
            }
            catch (Exception ex)
            {
                test.Error("Exception thrown while verifying Http Response for the request : " + ex.InnerException);
                test.Info("Response Code : " + resposeCode + " expected Response Code :" + respCode);
                test.Info("Description : " + description + " expected Description :" + message);
                throw ex;
            }
        }

        [Then(@"I go to verify the response with ResponseCode = ""(.*)"", message ""(.*)"", and for tag ""(.*)"" contains the correct values""(.*)""")]
        public void ThenIGoToVerifyTheResponseWithResponseCodeMessageAndForTags(string respCode, string message, string tag, List<string> values)
        {
            ExtentTest test = Settings.EnCompassExtentTest;

            string resposeCode = null;
            string description = null;
            string accountID = null;

            try
            {
                // add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
                XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";

                WebResponse response = Settings.Scenario["WebResponse"] as WebResponse;
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XDocument xmlResponse = XDocument.Parse(soapResult);
                    PrintXmlResponsePayloadAsDebugLog(xmlResponse, response);

                    // Save the ResponseCode and Description tag.
                    resposeCode = xmlResponse.Root.Descendants(soap + "ResponseCode").FirstOrDefault().Value;
                    description = xmlResponse.Root.Descendants(soap + "Description").FirstOrDefault().Value;
                    if (tag.ToLower() == "billingamount")
                    {
                        accountID = xmlResponse.Root.Descendants(soap + "AccountId").FirstOrDefault().Value;
                        Settings.Scenario["accountID"] = accountID;
                    }

                    IEnumerable<string> retrievedValues = xmlResponse.Root.Descendants(soap + tag).Select(n => n.Value);
                    if (tag.ToLower() == "billingamount" || tag.ToLower() == "transactionamount")
                    {
                        retrievedValues = retrievedValues.Select(n =>
                        {
                            string result = Convert.ToDouble(n).ToString("#0.00");
                            return result;
                        });
                    }

                    // Verify Response Code
                    Check.That(resposeCode.Trim()).Equals(respCode);
                    test.Info("response code matched expected code :" + respCode);

                    // Verify the Description tag.
                    Check.That(description.Trim()).Contains(message);
                    test.Info("description matched for web service");

                    foreach (string value in values)
                    {
                        bool res = retrievedValues.Contains(value.Trim());
                        Check.That(res).IsTrue();
                    }
                }
            }
            catch (Exception ex)
            {
                test.Error("Exception thrown while verifying Http Response for the request : " + ex.InnerException);
                test.Info("Response Code : " + resposeCode + " expected Response Code :" + respCode);
                test.Info("Description : " + description + " expected Description :" + message);
                throw ex;
            }
        }

		#endregion

		#region Private Helpers

		/// <summary>
		/// Private Method to modify API templates with Preset Data and Data injected from DataTable
		/// </summary>
		private void ModifyAPITemplateFromDataTable(string fileName, string dataSet, Table fieldsToModify,
			ExtentTest test, Tuple<string, string> _envFi)
		{
			try
			{
				// Rename the template file and copy it
				string fileAbsPath = FileUploadHelper.GetRenamedFileForAPIRequest(Settings, fileName);

				test.Info("Absolute path of renamed file to be modified: " + fileAbsPath);

				// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
				XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
				// Get XDocument for the file
				XDocument soapXmlReq = XDocument.Load(fileAbsPath);

				Tuple<string, string, string, string, string> _resultDataSet = AssignValuestoDataSet(dataSet);

				// OrgGrpLoginid, username, pwd, bankno, compno are mandatory values
				soapXmlReq.Root.Descendants(soap + "OrgGroupLoginId").FirstOrDefault().Value = _resultDataSet.Item1.ToString();
				soapXmlReq.Root.Descendants(soap + "Username").FirstOrDefault().Value = _resultDataSet.Item2.ToString();
				soapXmlReq.Root.Descendants(soap + "Password").FirstOrDefault().Value = _resultDataSet.Item3.ToString();

				// Bank No and Comp No are present in MOST requests but NOT ALL, hence masking exception here
				try
				{
					soapXmlReq.Root.Descendants(soap + "BankNumber").FirstOrDefault().Value = _resultDataSet.Item4.ToString();
					soapXmlReq.Root.Descendants(soap + "CompanyNumber").FirstOrDefault().Value = _resultDataSet.Item5.ToString();
				}
				catch (Exception)
				{
					test.Warning("BanoNo/CompNo not found in the Request");
				}

				// For each row in the Table
				fieldsToModify.Rows.ToList().ForEach(s =>
				{
					// Get the corresponding Value (in this case we're sure there's 1 value only hence elemetAt(0)
					s.TryGetValue(s.Keys.ElementAt(1), out string fieldValue);
					// Get the field Name as Key from the table
					s.TryGetValue(s.Keys.ElementAt(0), out string fieldName);

					// IF and only if the field value is not empty, set the value 
					if (!String.IsNullOrWhiteSpace(fieldValue))
					{
						// If input string contains the pattern {0} then replace all such patterns with unique values
						// in the Field Name. The same will be searched in the xml
						Regex regex = new Regex(@"[{][0-9][}]");
						foreach (Match match in regex.Matches(fieldValue))
						{
							fieldValue = string.Format(fieldValue, DateTime.UtcNow.ToString("MMddyyyymmssffff"));
						}

						// There are cases where we want special values to be entered, these are handled.
						// In those cases the input norm is {fieldname}. e.g. {InvoiceDate} in Create Merchant Log request
						regex = new Regex(@"[{](?i)[A-Z]+[}]");
						foreach (Match match in regex.Matches(fieldValue))
						{
							if (fieldValue.ToLowerInvariant().Contains("invoicedate"))
								fieldValue = DateTime.Now.ToString("O"); // e.g. 2019-11-15T14:12:57.3752134+05:30
							else if (fieldValue.ToLowerInvariant().Contains("fiproctype"))  // There are cases where Processor Type needs to be passed as input
								fieldValue = StringKeys.ProcessorTypePerFI(GlobalSettings.FI.ToString()).Item1;
							else if (fieldValue.ToLowerInvariant().Contains("randname"))  // Unique Fname & Lname
								fieldValue = (new Random().Next(100000000, 999999999)).ToString();
							else
								fieldValue = string.Format(fieldValue, DateTime.UtcNow.ToString("MMddyyyymmssffff")); // default fall back
						}

						// Replace 'today/tomorrow' with apt dates
						if (fieldValue.ToLower().Equals("today") || fieldValue.ToLower().Equals("tomorrow") || fieldValue.ToLower().Equals("yesterday"))
						{
							fieldValue = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(DateHelper.GetDateValueBasedOnInput(fieldValue)));
						}
						else if (fieldValue.ToLower().Equals("authaccount"))
						{
							StringKeys.AuthAccountNumbers.TryGetValue(_envFi, out string result);
							fieldValue = result;
						}
						else
						{
							// process fieldvalue via Global Transforms based on if its 'none'
							fieldValue = (fieldValue.ToString().ToLowerInvariant().Contains("none")) ? "" :
											  new GlobalTransforms(Settings).LookupInExternalDatasource(fieldValue).ToString();
						}

						// If field name has multiple hierarchies then we evaluate them
						// For e.g  <Invoice>  <description> .. </description> </invoice> will be used as 
						// Invice/description to uniquely identify it.  Since description field is present for other items
						if (fieldName.Contains("/"))
						{
							// If we have a hierarchy to maintain then we need to split the hierarchy to navigate to the node
							List<string> nodes = fieldName.Split('/').ToList();
							IEnumerable<XElement> rootDescendent = null;
							// Iterate all the descendent nodes drilling down to the last node and only then setting the value.
							for (int i = 0; i < nodes.Count; i++)
							{
								rootDescendent = (rootDescendent == null) ? soapXmlReq.Root.Descendants(soap + nodes.ElementAt(i)) : rootDescendent.Descendants(soap + nodes.ElementAt(i));
								if (i == nodes.Count - 1) // i.e for final node we set the value
									rootDescendent.FirstOrDefault().Value = fieldValue;
							}
						}
						else
						{
							// enter the processed field value
							soapXmlReq.Root.Descendants(soap + fieldName).FirstOrDefault().Value = fieldValue;
						}

						// Store each field modified in the file in scenario context
						Settings.Scenario[fieldName] = fieldValue;
						test.Info("Stored Field Value in Scenario Context named: " + fieldName + " with value :" +
										Settings.Scenario[fieldName].ToString());
					}
				});

				soapXmlReq.Save(fileAbsPath);
				test.Info("Saved file after modifying values");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Invokes the API to verify the Response Code and Description as per expected values
		/// </summary>
		private void VerifyAPIRespCodeAndDesc(string respCodeExpected, string expectedDescription, ExtentTest test,
            out string resposeCode, out string description)
        {
			if (String.IsNullOrEmpty(Settings.Scenario["WebResponse"].ToString()))
			{
				resposeCode = string.Empty;
				description = string.Empty;
				test.Debug("WebResponse in Scenario Ctx is Null or Empty");
				test.Skip("Skipping The Step as Env = PROD");
			}
			else
			{
				// Store the input params in scenario context. This is needed because at times retrieval call fails (as it needs more time to reflect the changes), then 
				// we need to call this step internally from the catch block
				Settings.Scenario["ExpRespCode"] = respCodeExpected;
				Settings.Scenario["ExpDesc"] = expectedDescription;

				// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
				XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";

				WebResponse response = Settings.Scenario["WebResponse"] as WebResponse;
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XDocument xmlResponse = XDocument.Parse(soapResult);
                    Settings.Scenario["XmlResponseObject"] = xmlResponse;
                    PrintXmlResponsePayloadAsDebugLog(xmlResponse, response);

                    // Save the ResponseCode and Description tag.
                    resposeCode = xmlResponse.Root.Descendants(soap + "ResponseCode").FirstOrDefault().Value;
                    description = xmlResponse.Root.Descendants(soap + "Description").FirstOrDefault().Value;

					// Verify Response Code
					try
					{
						Check.That(resposeCode.Trim().ToLowerInvariant()).Equals(respCodeExpected.Trim().ToLowerInvariant());
						test.Info("response code matched expected code :" + respCodeExpected);
					}
					catch (Exception)
					{
						test.Info("response code : " + resposeCode + " did not match expected code :" + respCodeExpected + " Retrying");
						// For Get Auth, Respo Code is the last value
						resposeCode = xmlResponse.Root.Descendants(soap + "ResponseCode").Last().Value;
						Check.That(resposeCode.Trim().ToLowerInvariant()).Equals(respCodeExpected.Trim().ToLowerInvariant());
						test.Info("response code : " + resposeCode + " matched expected code :" + respCodeExpected);
					}

					// Verify the Description tag.
					Check.That(expectedDescription.Trim().ToLowerInvariant()).Contains(description.Trim().ToLowerInvariant());
					test.Info("description matched for web service");
				}
			}
		}

        /// <summary>
        /// Verify specific values against nodes in the Response Xml
        /// </summary>
        private void VerifyAPIResponseValues(List<string> valuesToVerify, ExtentTest test, out string actualNodeValue,
            out string expNodeValue)
        {
			if (String.IsNullOrEmpty(Settings.Scenario["XmlResponseObject"].ToString()))
			{
				actualNodeValue = string.Empty;
				expNodeValue = string.Empty;
				test.Debug("XmlResponseObject in Scenario Ctx is Null or Empty");
				test.Skip("Skipping The Step as Env = PROD");
			}
			else
			{
				// add the xml name space for the tags whose values are to be replaced (in this case enc: points to http://aocsolutions.com/EncompassWebServices/
				XNamespace soap = "http://aocsolutions.com/EncompassWebServices/";
				XDocument xmlResponse = Settings.Scenario["XmlResponseObject"] as XDocument;

				// Verify Response values
				test.Info("Expected value for " + valuesToVerify.ElementAt(0) + " is :" + valuesToVerify.ElementAt(1));
				actualNodeValue = null;

				if (valuesToVerify.ElementAt(0).Contains("/"))
				{
					// If we have a hierarchy to maintain then we need to split the hierarchy to navigate to the node
					// and retrieve the value. For e.g.  <EmailAddresses> <string> ee@test.com </string> </EmailAddresses>
					List<string> nodes = valuesToVerify.ElementAt(0).Split('/').ToList();
					string nodeElement = nodes.ElementAt(0);
					// Process the node to handle indexes.  e.g. amount[3] will be returned as amount and 3
					processXMLHierarchyArrayFromInput(nodeElement, out string val, out int index);
					// Get the root descendents and filer out the specific index to get the XElement
					IEnumerable<XElement> rootDescendent = xmlResponse.Root.Descendants(soap + val);
					XElement rootDescendentElement = rootDescendent.ElementAt(index);
					nodes.Remove(nodeElement);
					// Iterate over the rest of the nodes
					for (int i = 0; i < nodes.Count; i++)
					{
						// process hierarchy for the node, get XElement
						processXMLHierarchyArrayFromInput(nodes.ElementAt(i), out val, out index);
						rootDescendent = rootDescendentElement.Descendants(soap + val);
						if (index >= 0)
						{
							rootDescendentElement = rootDescendent.ElementAt(index);

							// If it is the last node then we retrieve the value
							if (i == nodes.Count - 1)
							{
								actualNodeValue = rootDescendentElement.Value;
								break;
							}
						}
						else
						{
							// It is assumed that we will only fetch all values for the final node
							Check.That(i).IsEqualTo(nodes.Count - 1);
							string value = "";
							rootDescendent.ToList().Select(rd => rd.Value).ToList().ForEach(s => value += s);
							actualNodeValue = value;
							expNodeValue = new GlobalTransforms(Settings).LookupInExternalDatasource(valuesToVerify.ElementAt(1)).ToString();
							Check.That(actualNodeValue.ToLowerInvariant()).Contains(expNodeValue.ToLowerInvariant());
							test.Info("Actual value contains Expected value for :" + valuesToVerify.ElementAt(0));
							test.Info("Actual : " + actualNodeValue);
							test.Info("Expected : " + expNodeValue);
							return;
						}
					}
				}
				else
					actualNodeValue = xmlResponse.Root.Descendants(soap + valuesToVerify.ElementAt(0)).FirstOrDefault().Value;

				if (string.IsNullOrEmpty(valuesToVerify.ElementAt(1)))
				{
					expNodeValue = string.Empty;
					Check.That(xmlResponse.Root.Descendants(soap + valuesToVerify.ElementAt(0)).FirstOrDefault().Value).IsNotEmpty();
					test.Info("Expected value to verify is empty hence matched for non empty values in response.");
					return;
				}
				else
				{
					expNodeValue = new GlobalTransforms(Settings).LookupInExternalDatasource(valuesToVerify.ElementAt(1)).ToString();
					VerifyOutput(actualNodeValue.ToString(), expNodeValue, test);
				}
			}
        }

        /// <summary>
		/// Verify the actual and expected values satisy the corresponding criteria
		/// </summary>
        public void VerifyOutput(string actual,string expected, ExtentTest test)
        {
            //Checking if actual value lies in the passed values
            if (expected.Contains('|'))
            {
                var possibleOptions = expected.Split('|').ToList();
                Check.That(possibleOptions.Any(x => (x.ToLowerInvariant() == expected.ToString().ToLowerInvariant())));
                test.Info("Actual value: " + actual + " lies in the Expected value");
            }
            //Checking if actual value is greater than the passed value
            else if (expected.Contains('>'))
            {
                string result = expected.Replace(">", "");
                Check.That(Double.Parse(actual) > Double.Parse(result));
                test.Info("Actual value: " + actual + " is greater than Expected value: " + result);
            }
            else
            {
                Check.That(actual.ToLowerInvariant()).Equals(expected.ToLowerInvariant());
                test.Info("Actual value: " + actual + " is equal to the Expected value: " + expected);
            }

        }
		/// <summary>
		/// Process hierarchy in the nodeElement input from step def.
		/// e.g. amount[3] would be parsed and returned as amount , 3 as 2 diff values
		/// </summary>
		private void processXMLHierarchyArrayFromInput(string nodeElement, out string value, out int index)
		{
			value = String.Empty;
			index = 0;
            string MATCH_EXPRESSION = @"\[\w+\]";  //@"\[\d*\]";  
            var keyMatches = Regex.Matches(nodeElement, MATCH_EXPRESSION);
			if (keyMatches.Count == 1)
			{
				foreach (Match match in keyMatches)
				{
					value = nodeElement.Split('[')[0];
                    try { index = Int32.Parse(match.Value.Trim("[]".ToCharArray()).Replace('\'', ' ')); }
                    catch (FormatException)
                    {
                        // Number Format Excp will be thrown if the index is non-integer, so we check if its 'all' 
                        Check.That(match.Value.Trim("[]".ToCharArray()).Replace('\'', ' ').ToLowerInvariant()).Equals("all");
                        index = -1;
                    }
				}
			}
			else if(keyMatches.Count == 0)
			{
				value = nodeElement;				
			}
			else
			{
				throw new Exception("Regex match count > 1 in nodeElement :" + nodeElement);
			}
		}

        /// <summary>
		/// Returns the Dataset based the string passed
		/// </summary>
		private Tuple<string, string, string, string, string> AssignValuestoDataSet(string dataSet)
        {
            Tuple<string, string, string, string, string> _resultDataSet = null;
           
            if (dataSet == "PLogService")
            {
                _resultDataSet = StringKeys.APIDataSetForPLogService();
            }
			else
				_resultDataSet = StringKeys.APIDataSet();

			return _resultDataSet;
        }

		#endregion
			   
	}
}
