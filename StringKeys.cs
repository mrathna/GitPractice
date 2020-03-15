using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnCompass.Testing.Source
{
    public static class StringKeys
    {
        public enum ROLE
        {
            programadmin,
            cardholder,
            proxy,
            manageroptionrole,
            notmanageroptionrole,
            payablesrole,
            notpayablesrole
        }
        /// <summary>
        /// List of all FIs supported
        /// </summary>
        public enum FI
        {
            WEX,
            AKUSA,
            COMMERCE,
            CENTRAL,
            KEYBANK,
            PNC,
            REGIONS,
            SYNOVUS,
            USBANK,
            IBERIABANK,
            AMEX,
            GENERIC
        }

        /// <summary>
        /// FIs that needs to add new product type while creating Org in the SUGA flow
        /// </summary>
        public static List<string> FIsRequiringReduceCreditLimitOnMLogClose
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString(),
                    FI.AKUSA.ToString(),
                    FI.CENTRAL.ToString(),
                    FI.KEYBANK.ToString(),
                    FI.PNC.ToString(),
                    FI.REGIONS.ToString(),
                    FI.SYNOVUS.ToString(),
                    FI.USBANK.ToString(),
                    FI.IBERIABANK.ToString(),
                };
            }
        }

        /// <summary>
        /// FIs that needs to add new product type while creating Org in the SUGA flow
        /// </summary>
        public static List<string> FIsRequiringOrgLevelChangesForSUGA
        {
            get
            {
                return new List<string>() { FI.COMMERCE.ToString(), FI.CENTRAL.ToString(), FI.KEYBANK.ToString(), FI.REGIONS.ToString(), FI.IBERIABANK.ToString(), FI.USBANK.ToString() };
            }
        }

        /// <summary>
        /// List of FI that needs Unique Identifier and Alternate Ph no to be filled in mandatorily
        /// while updating Card Account.
        /// </summary>
        public static List<string> FIsRequiringUniqueIdAndAltPh
        {
            get
            {
                return new List<string>() { FI.COMMERCE.ToString(), FI.AKUSA.ToString(), FI.CENTRAL.ToString(), FI.KEYBANK.ToString(),
                    FI.PNC.ToString(), FI.REGIONS.ToString(), FI.USBANK.ToString(), FI.IBERIABANK.ToString() };
            }
        }

        /// <summary>
        /// List of FI that needs Unique Identifier and Alternate Ph no to be filled in mandatorily
        /// while updating Card Account.
        /// </summary>
        public static List<string> FIsRequiringDisputeReplyEmailForOrg
        {
            get
            {
                return new List<string>() { FI.WEX.ToString(), FI.GENERIC.ToString() };
            }
        }

        /// <summary>
        /// FIs which does not allow users to modify the Business Address while editing/creating card accounts (Non-Suga)
        /// </summary>
        public static List<string> FIsBlockingCardUpdateOnAddress
        {
            get
            {
                return new List<string>() { FI.SYNOVUS.ToString(), FI.AMEX.ToString() };
            }
        }

        /// <summary>
        /// FIs which does not have invoice parser JTA
        /// </summary>
        public static List<string> FIsNotHavingInvoiceParserJTA
        {
            get
            {
                return new List<string>() { FI.COMMERCE.ToString() };
            }
        }

		/// <summary>
		/// List of FI that needs Instant Approval to be filled in mandatorily
		/// while creating user Account.
		/// </summary>
		public static List<string> FIsRequiringInstantApprovalWhileCreatingUser
		{
			get
			{
				return new List<string>() { FI.IBERIABANK.ToString(), FI.KEYBANK.ToString(), FI.WEX.ToString(), FI.CENTRAL.ToString(), FI.USBANK.ToString(), FI.GENERIC.ToString() };
			}
		}

        /// <summary>
        /// List of FI that Allows Purchase Log Limits at Org Level.
        /// Confirmed by Rebecca Clark.
        /// </summary>
        public static List<string> FIsSupportingPlogLimits
        {
            get
            {
                return new List<string>() { FI.WEX.ToString(), FI.PNC.ToString(), FI.CENTRAL.ToString(), FI.GENERIC.ToString() };
            }
        }

        /// <summary>
        /// List of FI that needs Instant Approval to be filled in mandatorily
        /// while creating user Account.
        /// </summary>
        public static List<string> FIsNotRequiringInstantApprovalInAlerts
        {
            get
            {
                return new List<string>() { FI.USBANK.ToString(), FI.COMMERCE.ToString() };
            }
        }

        /// <summary>
        /// List of FI that needs second confirmation after modify a card account.
        /// </summary>
        public static List<string> FIsWithNoChangesAtCards
        {
            get
            {
                return new List<string>() { FI.SYNOVUS.ToString() };
            }
        }

        /// <summary>
        /// List of FI that With the same xpath Financial Code Information.
        /// </summary>
        public static List<string> FIsWithFinancialCodeInformation
        {
            get
            {
                return new List<string>() { FI.SYNOVUS.ToString(), FI.USBANK.ToString(), FI.IBERIABANK.ToString(), FI.PNC.ToString(), FI.AKUSA.ToString(), FI.CENTRAL.ToString() };
            }
        }

        /// <summary>
        /// FIs that set the SSN field when confirm the new password
        /// </summary>
        public static List<string> FIsSetSNNandConfirm
        {
            get
            {
                return new List<string>() { FI.WEX.ToString(), FI.KEYBANK.ToString(), FI.CENTRAL.ToString(), FI.SYNOVUS.ToString(), FI.IBERIABANK.ToString() };
            }
        }

        /// <summary>
        /// FIs that needs to fill up field of primary phone while creating an user
        /// </summary>
        public static List<string> FIsRequiringAddPrimaryPhone
        {
            get
            {
                return new List<string>() { FI.KEYBANK.ToString(), FI.USBANK.ToString(), FI.REGIONS.ToString() };
            }
        }

        /// <summary>
        /// FIs that needs to fill up field of alternative phone
        /// </summary>
        public static List<string> FIsRequiringAddAlternativePhone
        {
            get
            {
                return new List<string>() { FI.AKUSA.ToString() };
            }
        }

        /// <summary>
        /// FIs that needs to fill up field of Activation Code while creating a Card
        /// </summary>
        public static List<string> FIsRequiringAddActivationCode
        {
            get
            {
                return new List<string>() { FI.KEYBANK.ToString(), FI.COMMERCE.ToString(), FI.PNC.ToString(), FI.USBANK.ToString(), FI.CENTRAL.ToString(), FI.REGIONS.ToString(), FI.AKUSA.ToString(), FI.IBERIABANK.ToString() };
            }
        }

        /// <summary>
        /// FIs that do not have Alert after check true on reduceCreditLimitOnClose
        /// </summary>
        public static List<string> FIsMerchantLogSettingsAcceptAlert
        {
            get
            {
                return new List<string>() { FI.PNC.ToString(), FI.USBANK.ToString(), FI.AKUSA.ToString() };
            }
        }

        /// <summary>
        /// FIs that have does not allow the user to Edit Card Name/Adress and/or Update it
        /// </summary>
        public static List<string> FIsCardsUpdateBlocked
        {
            get
            {
                return new List<string>() { FI.SYNOVUS.ToString(), FI.AMEX.ToString() };
            }
        }

        /// <summary>
        /// FIs that have Product type as T&E (indiv pay)
        /// </summary>
        public static List<string> FIsWithTEProductType
        {
            get
            {
                return new List<string>() { FI.REGIONS.ToString(), FI.KEYBANK.ToString() };
            }
        }

        /// <summary>
        /// FIs that have differens Account Status Card Profile
        /// </summary>
        public static IDictionary<string, string> FIsAccountStatusCardProfile { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "ACTIVE" },
            { FI.AKUSA.ToString(), "ACTIVE" },
            { FI.COMMERCE.ToString(), "ACTIVE" },
            { FI.CENTRAL.ToString(), "Open Account, no block or reclass" },
            { FI.KEYBANK.ToString(), "Open Account, no block or reclass" },
            { FI.PNC.ToString(), "ACTIVE" },
            { FI.SYNOVUS.ToString(), "ACTIVE" },
            { FI.USBANK.ToString(), "Open" },
            { FI.IBERIABANK.ToString(), "Open Account, no block or reclass" },
            { FI.GENERIC.ToString(), "NotifyMe" }
        };

        /// <summary>
		/// FIs that have differens Remit template message
		/// </summary>
		public static IDictionary<string, string> FIsRemitTemplateMessage { get; } = new Dictionary<string, string>()
        {
            { FI.COMMERCE.ToString(), "NotifyMe Plain Text Remit" },
            { FI.USBANK.ToString(), "Pull HTML Remit" },
            { FI.IBERIABANK.ToString(), "Pull HTML Remit" }
        };

        /// <summary>
        /// FIs that have different field on Registration user page
        /// </summary>
        public static IDictionary<string, string> FIsRegistrationUserID { get; } = new Dictionary<string, string>()
        {
            { FI.PNC.ToString(), "Unique identifier is a required value" },
            { FI.COMMERCE.ToString(), "Unique identifier is a required value" },
            { FI.AKUSA.ToString(), "Activation ID is a required value" }

        };

        /// <summary>
        /// FIs that have differens Account Status for closed Card Profile
        /// </summary>
        public static IDictionary<string, string> FIsAccountStatusClosedCardProfile { get; } = new Dictionary<string, string>()
        {
            { FI.SYNOVUS.ToString(), "M9 - PERM INACTIVE" },
            { FI.AKUSA.ToString(), "V9 - CLOSED" },
            { FI.USBANK.ToString(), "V9 - Closed - Voluntary" }
        };

        /// <summary>
        /// FIs that have differens values to Bank Check Payment Method
        /// </summary>
        public static IDictionary<string, string> FIsCheckPaymentMethod { get; } = new Dictionary<string, string>()
        {
            { FI.SYNOVUS.ToString(), "Check Payment" }
        };

        /// <summary>
        /// FIs that have differens values to Pay Me Payment Method
        /// </summary>

        public static IDictionary<string, string> FIsPayMePaymentMethod { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "PayMe" },
            { FI.AKUSA.ToString(), "VendorAutoPay" },
            { FI.COMMERCE.ToString(), "PayMe" },
            { FI.CENTRAL.ToString(), "Push" },
            { FI.KEYBANK.ToString(), "Push" },
            { FI.PNC.ToString(), "Push" },
            { FI.SYNOVUS.ToString(), "Deposit Pay" },
            { FI.USBANK.ToString(), "Push" },
            { FI.IBERIABANK.ToString(), "Push" },
            { FI.REGIONS.ToString(), "Push" },
            { FI.GENERIC.ToString(), "PayMe" }
        };

        /// <summary>
		/// FIs that have differens values to global group Name
		/// </summary>
		public static IDictionary<string, string> FIsGlobalMCCGroupName { get; } = new Dictionary<string, string>()
        {
            { FI.SYNOVUS.ToString(), "MADISONSC1" }
        };

        /// <summary>
        /// Dictionary object of various AP Branding options per FI
        /// </summary>
        public static IDictionary<string, string> APBrandingPerFI { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "NotifyMe" },
            { FI.AKUSA.ToString(), "RemitPay" },
            { FI.COMMERCE.ToString(), "ProcessMe" },
            { FI.CENTRAL.ToString(), "Pull" },
            { FI.KEYBANK.ToString(), "Pull" },
            { FI.PNC.ToString(), "Pull" },
            { FI.REGIONS.ToString(), "Pull" },
            { FI.SYNOVUS.ToString(), "Notify Pay" },
            { FI.USBANK.ToString(), "Pull" },
            { FI.IBERIABANK.ToString(), "Pull" },
            { FI.AMEX.ToString(), "Pull" },
            { FI.GENERIC.ToString(), "NotifyMe" }
        };

        /// <summary>
        /// Dictionary object of various Bank Numbers per FI
        /// Change bank number for Cantral to BA (Brian said: UAT it doesnt look like we have mccgs set up for FK but there are for BA)
        /// </summary>
        public static IDictionary<string, string> BankNoPerFI { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "2272" },
            { FI.AKUSA.ToString(), "7802" },
            { FI.COMMERCE.ToString(), "2748" },
            { FI.CENTRAL.ToString(), "BA" },
            { FI.KEYBANK.ToString(), "KB" },
            { FI.PNC.ToString(), "1940" },
            { FI.REGIONS.ToString(), "8190" },
            { FI.SYNOVUS.ToString(), "1038" },
            { FI.USBANK.ToString(), "3752" },
            { FI.IBERIABANK.ToString(), "IB" },
            { FI.AMEX.ToString(), "AMEX" },
            {FI.GENERIC.ToString(), "2272" }
        };

        /// <summary>
        /// Dictionary object of Purchase Log Role
        /// </summary>
        public static IDictionary<string, string> RolesPurchaseLog { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "PURCHASE LOG" },
            { FI.AKUSA.ToString(), "OTO PAY" },
            { FI.COMMERCE.ToString(), "BUYERS LOG" },
            { FI.CENTRAL.ToString(), "PURCHASE LOG" },
            { FI.KEYBANK.ToString(), "PURCHASE LOG" },
            { FI.PNC.ToString(), "PURCHASE LOG" },
            { FI.REGIONS.ToString(), "PURCHASE LOG" },
            { FI.SYNOVUS.ToString(), "PURCHASE LOG" },
            { FI.USBANK.ToString(), "PURCHASE LOG" },
            { FI.IBERIABANK.ToString(), "PURCHASE LOG" },
            { FI.AMEX.ToString(), "PURCHASE LOG" },
            { FI.GENERIC.ToString(), "PURCHASE LOG" }
        };

        /// <summary>
        /// Dictionary object of MccGroup
        /// </summary>
        public static IDictionary<string, string> MccGroup { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "ALL (Global)" },
            { FI.AKUSA.ToString(), "CASH (Global)" },
            { FI.COMMERCE.ToString(), "CASH (Global)" },
            { FI.CENTRAL.ToString(), "All (Global)" },
            { FI.KEYBANK.ToString(), "All (Global)" },
            { FI.PNC.ToString(), "CASH (Global)" },
            { FI.SYNOVUS.ToString(), "AIRLINE (Global)" },
            { FI.USBANK.ToString(), "CPM ALL (Global)" },
            { FI.IBERIABANK.ToString(), "All (Global)" },
            { FI.GENERIC.ToString(), "AIRLINE (Global)" }
        };

        /// <summary>
        /// FI's that required 7 didigts for hierarchy
        /// </summary>
        public static List<string> HierarchyWithSevenDigits
        {
            get
            {
                return _hierarchyWithSevenDigits;
            }
        }

        private static List<string> _hierarchyWithSevenDigits
        {
            get
            {
                return new List<string>() { FI.KEYBANK.ToString(), FI.COMMERCE.ToString(), FI.CENTRAL.ToString(), FI.REGIONS.ToString(), FI.IBERIABANK.ToString(), FI.AMEX.ToString() };
            }
        }

        /// <summary>
        /// FIs without add new card button
        /// </summary>
        public static List<string> FIsWithoutAddCard
        {
            get
            {
                return new List<string>() { FI.SYNOVUS.ToString(), FI.AKUSA.ToString(), FI.PNC.ToString(), FI.AMEX.ToString() };
            }
        }

        /// <summary>
        /// Options for SUGA Product/SubProduct
        /// </summary>
        public static Dictionary<string, string> SUGAProductSubProduct { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "SU2/STD" },
            { FI.AKUSA.ToString(), "SU2/STD" },
            { FI.COMMERCE.ToString(), "SU2/STD" },
            { FI.CENTRAL.ToString(), "SUGA (SU2/STD)" },
            { FI.KEYBANK.ToString(), "SU2/STD" },
            { FI.PNC.ToString(), "SU2/STD" },
            { FI.REGIONS.ToString(), "SU2/TST" },
            { FI.SYNOVUS.ToString(), "SU2/STD" },
            { FI.USBANK.ToString(), "SU2/STD" },
            { FI.IBERIABANK.ToString(), "SU2/STD" },
            { FI.AMEX.ToString(), "SU2/STD" },
            { FI.GENERIC.ToString(), "SU2/STD" }
        };

		/// <summary>
		/// Merchant names with code to be used when running test in production env for SUGA Flows
		/// </summary>
		//TODO needs to change the merchant name with card available for SYNOVUS and AMEX (Brian or Chloe)
		public static Dictionary<string, string> ProductionMerchantsSugaFLow { get; } = new Dictionary<string, string>()
		{
			{ FI.WEX.ToString(), "SUGA" },
			{ FI.AKUSA.ToString(), "TESTSUGAPIN2" },
			{ FI.COMMERCE.ToString(), "SUGA Merchant" },
			{ FI.CENTRAL.ToString(), "UpdatedAutomationName" },
			{ FI.KEYBANK.ToString(), "Suga Merchant" },
			{ FI.PNC.ToString(), "AAAAArburnstestAddMerchantTest" },
			{ FI.REGIONS.ToString(), "001_Deploy_Merchant" },
			{ FI.SYNOVUS.ToString(), "No Cards available" },
			{ FI.USBANK.ToString(), "DSMer1" },
			{ FI.IBERIABANK.ToString(), "SUGA Merchant" },
			{ FI.AMEX.ToString(), "19.1Merchant" },
			{ FI.GENERIC.ToString(), "SugaCLJ1" }
		};

		/// <summary>
		/// Merchant names with code to be used when running test in production env
		/// </summary>
		public static Dictionary<string, string> ProductionMerchantsWithCodeByFI { get; } = new Dictionary<string, string>()
		{
			{ FI.WEX.ToString(), "EE Test Vendor (EE6065)" },
			{ FI.AKUSA.ToString(), "ProcMe1 (ProcMe1)" },
			{ FI.COMMERCE.ToString(), "ProcMe1 (procme1)" },
			{ FI.CENTRAL.ToString(), "UpdatedAutomationName (test1032)" },
			{ FI.KEYBANK.ToString(), "073116_VendorPlus (073116_VendorPlus)" },
			{ FI.PNC.ToString(), "ProcMe1 (ProcMe1)" },
			{ FI.REGIONS.ToString(), "ProcMe1 (ProcMe1)" },
			{ FI.SYNOVUS.ToString(), "Ranorex Automated Tests (123456789)" },
			{ FI.USBANK.ToString(), "assigned_card (assigned_card)" },
			{ FI.IBERIABANK.ToString(), "SUGA Merchant (SUGA Merchant)" },
			//{ FI.AMEX.ToString(), "003_LiveMerchant (003_LiveMerchant)" }, // AMEX is not valid for AP Traditional flow
			{ FI.GENERIC.ToString(), "EE Test Vendor" }
        };

        /// <summary>
        /// Orgs to be used when running test in production env
        /// </summary>
        public static Dictionary<string, string> ProductionOrgs { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "EncQATest01" },
            { FI.AKUSA.ToString(), "SFTEST" },
            { FI.COMMERCE.ToString(), "APTEST" },
            { FI.CENTRAL.ToString(), "Encompass QA" },
            { FI.KEYBANK.ToString(), "AOCTESTORG AP" },
            { FI.PNC.ToString(), "ACTIVEPAYTEST33333" },
            { FI.REGIONS.ToString(), "AOC_AP" },
            { FI.SYNOVUS.ToString(), "CORPBILL" },
            { FI.USBANK.ToString(), "AOC_3752" },
            { FI.IBERIABANK.ToString(), "AOC_AP" },
            { FI.AMEX.ToString(), "WEX_LiveTestOrg" },  // "003_LiveOrgRegression"
            { FI.GENERIC.ToString(), "WEXTESTEPAY" }
        };

        /// <summary>
        /// Orgs to be used when running test in production env for purchase logs ONLY.
        /// For now, the test is only applicable for WEX.
        /// </summary>
        public static Dictionary<string, string> ProductionOrgsForPlog { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "EncQATest01" }
        };

		/// <summary>
		/// Orgs to be used when running test in production env
		/// </summary>
		public static Dictionary<string, string> ProductionOrgCompNos { get; } = new Dictionary<string, string>()
		{
			{ FI.WEX.ToString(), "9999113" },
			{ FI.AKUSA.ToString(), "90068" },
			{ FI.COMMERCE.ToString(), "0605706" },
			{ FI.CENTRAL.ToString(), "9999801" },
			{ FI.KEYBANK.ToString(), "9999902" },
			{ FI.PNC.ToString(), "33333" },
			{ FI.REGIONS.ToString(), "7106951" },
			{ FI.SYNOVUS.ToString(), "22565" },
			{ FI.USBANK.ToString(), "10044" },
			{ FI.IBERIABANK.ToString(), "8888801" },
			{ FI.AMEX.ToString(), "1000271" },  // 1000090
            { FI.GENERIC.ToString(), "10450" }
        };

        /// <summary>
        /// Orgs to be used when running test in production env
        /// </summary>
        public static Dictionary<string, string> ProductionOrgCompNosForPlog { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "9999113" },
            { FI.AKUSA.ToString(), "" },
            { FI.COMMERCE.ToString(), "" },
            { FI.CENTRAL.ToString(), "" },
            { FI.KEYBANK.ToString(), "" },
            { FI.PNC.ToString(), "" },
            { FI.REGIONS.ToString(), "" },
            { FI.SYNOVUS.ToString(), "" },
            { FI.USBANK.ToString(), "" },
            { FI.IBERIABANK.ToString(), "" },
            { FI.AMEX.ToString(), "" },
            { FI.GENERIC.ToString(), "" }
        };

        /// <summary>
        /// User names to be used when running test in production env
        /// </summary>
        public static Dictionary<string, string> ProductionUsernamesByFI { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "ranorextest" },
            { FI.AKUSA.ToString(), "blockertest" },
            { FI.COMMERCE.ToString(), "BlockerPA" },
            { FI.CENTRAL.ToString(), "progadminor" },
            { FI.KEYBANK.ToString(), "cjtest1234" },
            { FI.PNC.ToString(), "blockertest" },
            { FI.REGIONS.ToString(), "cjcardholder" },
            { FI.SYNOVUS.ToString(), "kevaughn1" },
            { FI.USBANK.ToString(), "kevaughn1" },
            { FI.IBERIABANK.ToString(), "ranorextest" },
            { FI.AMEX.ToString(), "HLVAutomation" },  // "LiveorgUserOne"
            { FI.GENERIC.ToString(), "ranorextest" }
        };


        public static List<string> BankCheckPayments
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString(),
                    FI.COMMERCE.ToString(),
                    FI.CENTRAL.ToString(),
                    FI.REGIONS.ToString(),
                    FI.GENERIC.ToString()
                };
            }
        }

        public static List<string> BankCheckFee
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString(),
                    FI.CENTRAL.ToString(),
                    FI.GENERIC.ToString()
                };
            }
        }

        /// <summary>
        /// List of FIs which needs explicit ARX enablement
        /// </summary>
        public static List<string> EnableARXExplicit
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString(), FI.GENERIC.ToString()
                };
            }
        }

        /// <summary>
        /// List of FIs which allow Enabling Effective dates in Merchant Log Settings
        /// </summary>
        public static List<string> EnableMlogEffectiveDates
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString(), FI.GENERIC.ToString()
                };
            }
        }

        public static List<string> FIsWithoutSSN
        {
            get
            {
                return new List<string>()
                {
                    FI.AKUSA.ToString(),FI.COMMERCE.ToString(), FI.PNC.ToString()
                };
            }
        }

        /// <summary>
        /// To get InvoiceNumber, InvoiceDate, DiscountAmount, TaxAmount and Amount inside the ARX should create the Remit Template
        /// </summary>
        public static List<string> FIsShouldCreateRemitTemplate
        {
            get
            {
                return new List<string>()
                {
                    FI.REGIONS.ToString()
                };
            }
        }

        /// <summary>
        /// Notifications Report have different names accross the FIs. This dictionary stores their names of each according to its FI.
        /// TODO: Tested only for: WEX, IBERIBANK.
        /// Please update if you find a different value for the remaining FIs.
        /// </summary>
        public static Dictionary<string, string> NotificationReportNamePerFI { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "Notifications" },
            { FI.AKUSA.ToString(), "Notifications" },
            { FI.COMMERCE.ToString(), "Notifications" },
            { FI.CENTRAL.ToString(), "Notifications" },
            { FI.KEYBANK.ToString(), "Notifications" },
            { FI.PNC.ToString(), "Notifications" },
            { FI.REGIONS.ToString(), "Notifications" },
            { FI.SYNOVUS.ToString(), "Notifications" },
            { FI.USBANK.ToString(), "Notifications" },
            { FI.IBERIABANK.ToString(), "Notification Report" },
            { FI.AMEX.ToString(), "Notifications" },
            { FI.GENERIC.ToString(), "Notification Report" }
        };

        /// </summary>
        /// All Transactions Report files have different names accross the FIs. This dictionary stores their names of each
        /// according to its FI.
        /// </summary>
        public static Dictionary<string, string> AllTransactionsFileNamePerFI { get; } = new Dictionary<string, string>()
        {
            { FI.WEX.ToString(), "All_Transactions_Record" },
            { FI.AKUSA.ToString(), "All_Transactions_Record" },
            { FI.COMMERCE.ToString(), "All_Transactions_Record" },
            { FI.CENTRAL.ToString(), "All_Transactions_Summary" },
            { FI.KEYBANK.ToString(), "All_Transactions_Record" },
            { FI.PNC.ToString(), "All_Transactions_Record" },
            { FI.REGIONS.ToString(), "All_Transactions_Record" },
            { FI.SYNOVUS.ToString(), "All_Transactions_Record" },
            { FI.USBANK.ToString(), "All_Transactions_Summary" },
            { FI.IBERIABANK.ToString(), "All_Transactions_Record" },
            { FI.AMEX.ToString(), "All_Transactions_Record" },
            { FI.GENERIC.ToString(), "All_Transactions_Summary" }
        };

        /// </summary>
        /// List of all columns from Merchant Log Grid
        /// </summary>
        public static List<string> MLogGridColumns {
            get
            {
                return new List<string>()
                {
                    "Unique ID","Merchant","Merchant Code","Customer Code","Amount","Billing Currency","Status",
                    "Last Status Change Date","Last Operation","Payment Method","Created Date","Created By",
                    "Account Number","Expiration Date","Expiration Reminder Date","Check Number","Merchant Category Code",
                    "Local Currency Amount","Local Currency","Transaction Amount","Balance","Delivery Method",
                    "Email Delivery Status","Fax Delivery Status","Source","Invoice File Name"
                };
            }
        }

        /// </summary>
        /// List of all columns from Merchant Log Grid
        /// </summary>
        public static List<string> MLogHistoryGridColumns
        {
            get
            {
                return new List<string>()
                {
                    "Change Date","Modifield By","Last Operation","Merchant Code","Merchant Name", "Account Number","Local Currency Amount",
                    "Amount", "Payment Method", "Exact Match", "Resend", "Expiration Reminder Date", "Expiration Date", "Close Note", "Payment Assignment"
                    ,"PIN Sent"
                };
            }
        }

        /// </summary>
        /// List of all columns from Merchant History Grid
        /// </summary>
        public static List<string> MerchantHistoryGridColumns
        {
            get
            {
                return new List<string>()
                {
                    "Modifield Date","Modifield By","Merchant Name", "Merchant Code","Customer Account Number","Currency","Active",
                    "Status change reason","Email Address","Cc Email Address(es)","Phone Number","Fax","Taxpayer ID","Address Line 1",
                    "Address Line 2","Address Line 3","Address Line 4","City", "State or Province","ZIP or Postal Code", "Attention",
                    "Country or Region", "One Card","Program"
                };
            }
        }

        /// </summary>
        /// List of all columns from Merchant History Grid
        /// </summary>
        public static List<string> TransactionGridColumns
        {
            get
            {
                return new List<string>()
                {
                    "Post Date","Transaction Date","Billing Amount","Merchant","Card","Name on Card","Employee ID","Disputed",
                    "Split","Extracted"
                };
            }
        }

        /// </summary>
        /// List of all columns from Third Party Grid
        /// </summary>
        public static List<string> ListThirdPartyGridColumns
        {
            get
            {
                return new List<string>()
                {
                    "Actions","Name","Website","Email","Granted On"
                };
            }
        }

        /// <summary>
        /// This was created in order to support the High Level Test which is required the usage of pre-defined orgs and accounts.
        /// DONT USE THIS AGAINST QA ENV.
        /// </summary>
        public static Tuple<string, int> RecentActivitySettings(string fi)
        {
            switch (fi.ToLowerInvariant())
            {
                case "commerce":
                    return new Tuple<string, int>("TESTGROUP", 0017);
                case "wex":
                    return new Tuple<string, int>("WEXTESTEPAY", 8578);
                case "pnc":
                    return new Tuple<string, int>("ACTIVEPAYTEST33333", 9915);
                case "synovus":
                    return new Tuple<string, int>("Corpbill", 4711);
                case "akusa":
                    return new Tuple<string, int>("SFTEST", 0253);
                case "keybank":
                    return new Tuple<string, int>("AOCTESTORG AP", 0679);
                case "regions":
                    return new Tuple<string, int>("AOC_AP", 6889);
                case "central":
                    return new Tuple<string, int>("AOC_AP", 2833);
                case "usbank":
                    return new Tuple<string, int>("AOC_3752", 6562);
                case "iberiabank":
                    return new Tuple<string, int>("AOC_AP", 8764);
                default: return null;
            }
        }

        #region Processor Type

        /// <summary>
        /// Enum for Processor Types
        /// </summary>
        public enum ProcessorType
        {
            HP,
            TS1,
            TS2NorthAmerica,
            TS2APAC,
            TS2EUROPE,
            AOC,
            AMEX
        }

        /// <summary>
        /// This was created based on processor type (HP/TS1/TS2) per FI
        /// </summary>
        public static Tuple<string, string> ProcessorTypePerFI(string fi)
        {
            switch (fi.ToLowerInvariant())
            {
                case "commerce":
                    return new Tuple<string, string>(ProcessorType.TS2NorthAmerica.ToString(), "");
                case "wex":
                    return new Tuple<string, string>(ProcessorType.TS1.ToString(), ProcessorType.TS2NorthAmerica.ToString());
                case "amex":
                    return new Tuple<string, string>(ProcessorType.AMEX.ToString(), "");
                case "pnc":
                    return new Tuple<string, string>(ProcessorType.TS1.ToString(), "");
                case "synovus":
                    return new Tuple<string, string>(ProcessorType.TS1.ToString(), "");
                case "akusa":
                    return new Tuple<string, string>(ProcessorType.TS1.ToString(), "");
                case "keybank":
                    return new Tuple<string, string>(ProcessorType.HP.ToString(), "");
                case "regions":
                    return new Tuple<string, string>(ProcessorType.TS2NorthAmerica.ToString(), "");
                case "central":
                    return new Tuple<string, string>(ProcessorType.HP.ToString(), "");
                case "usbank":
                    return new Tuple<string, string>(ProcessorType.TS1.ToString(), ProcessorType.TS2NorthAmerica.ToString());
                case "iberiabank":
                    return new Tuple<string, string>(ProcessorType.HP.ToString(), "");
                default: return null;
            }
        }

        public static List<string> SimpleSingleCardAddScreenProcessor
        {
            get
            {
                return new List<string>() { ProcessorType.TS2NorthAmerica.ToString() };
            }
        }

        #endregion

        #region Card Ordering Data

        /// <summary>
        /// Enum of various types of Card Ordering Type.  Make Sure the Enum Value NEVER exceeds 25 chars as 
        /// its used as Addr Line 2 which has the char limit
        /// </summary>
        public enum CardOrderingTypes
        {
            SingleCardAdd_NONSUGA,
            NewAcntUpldACE_NONSUGA,
            AutoInventory_SUGA,
            MerchantCardAdd_NONSUGA,
            ExactAuth_SUGA,
            MchntCrdAddUpld_NONSUGA
        }

        /// <summary>
        /// Mapping of -  Comp No, OrgId for each FI to be used for Card Ordering Flow based on ProcessorType 
        /// Note  - For MerchantCardAdd_NONSUGA  we're using SUGA org, the reason being the Non-Suga org is not set up for AP.
        /// Per confirmation from Roger - using SUGA org is fine.
        /// Returns Tuple of CompNo and OrgId
        /// </summary>
        public static Tuple<string, string> ProcessorVsOrgMappingPerFI(string fi, string procType, string _CardOdrType)
        {
            switch (fi.ToLowerInvariant())
            {
                case "keybank":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase)
                          )
                        return new Tuple<string, string>("9999901", "AOCTESTORG T&E");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.MerchantCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.ExactAuth_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("9999902", "AOCTESTORG AP");
                    else
                        return null;
                case "wex":
                    if (procType.ToUpperInvariant().Equals(ProcessorType.TS1.ToString()))
                        return new Tuple<string, string>("00013", "PRODUCT13");
                    else if (procType.ToUpperInvariant().Equals(ProcessorType.TS2NorthAmerica.ToString()))
                        return new Tuple<string, string>("9999905", "Test_MC_Visa_18.1");
                    else
                        return null;
                case "regions":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase)
                          )
                        return new Tuple<string, string>("1299902", "AOC_TE");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.MerchantCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("1299901", "AOC_AP");
                    else
                        return null;
                case "central":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("9999902", "AOC_TE");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.ExactAuth_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("9999901", "AOC_AP");
                    else
                        return null;
                case "iberiabank":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("8888802", "AOC_TE");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.ExactAuth_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("8888801", "AOC_AP");
                    else
                        return null;
                case "commerce":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("0605707", "TESTIND");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.MerchantCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("0605706", "APTEST");
                    else
                        return null;
                case "synovus":
                    if (_CardOdrType.Equals(CardOrderingTypes.SingleCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.NewAcntUpldACE_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                        _CardOdrType.Equals(CardOrderingTypes.MerchantCardAdd_NONSUGA.ToString(), StringComparison.InvariantCultureIgnoreCase)
                          )
                        return new Tuple<string, string>("22565", "CORPBILL");
                    else if (_CardOdrType.Equals(CardOrderingTypes.AutoInventory_SUGA.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        return new Tuple<string, string>("02569", "APTestSUGA");
                    else
                        return null;
                case "pnc":
                    return new Tuple<string, string>("33333", "ACTIVEPAYTEST33333");
                case "usbank":
                    return new Tuple<string, string>("13508", "BLAMAXMCSUA");

                default: return null;
            }
        }

        #endregion

        public enum EFundsFundingSource
        {
            ACH,
            WireDrawDown,
            PushACH,
            Wire
        }

        /// <summary>
        /// FIs With Multiple Currencies Enabled
        /// </summary>
        public static List<string> FIWithMultipleCurrenciesEnabled
        {
            get
            {
                return new List<string>()
                {
                    FI.WEX.ToString()
                };
            }
        }

        /// <summary>
        /// Funds Funding Source
        /// </summary>
        public static Dictionary<EFundsFundingSource, string> FundsFundingSource { get; } = new Dictionary<EFundsFundingSource, string>()
        {
            { EFundsFundingSource.ACH, "ACH" },
            { EFundsFundingSource.WireDrawDown, "Wire Draw Down" },
            { EFundsFundingSource.PushACH, "Push ACH" },
            { EFundsFundingSource.Wire, "Wire" }
        };

        public static List<string> FilesRequiringJTA =>
            new List<string>() { "MlogInvoice_upload.txt" };


        /// <summary>
        /// List of Report Wizard Focus without hierarchy button at step 6;
        /// </summary>
        public static List<string> FocusWithoutHierarchyButton
        {
            get
            {
                return new List<string>()
                {
                    "transaction envelope"
                };
            }
        }

        #region API Data

        /// <summary>
		/// Tuple Data Set per FI for PLog Service tests. This is static data in UAT.
		/// item 1 : OrgGroupLoginId
		/// item 2 : Username
		/// item 3 : Password
		/// item 4 : BankNumber
		/// item 5 : CompanyNumber
		/// </summary>
		public static Tuple<string, string, string, string, string> APIDataSetForPLogService()
        {
			Tuple<string, string> _fiEnv = new Tuple<string, string>(GlobalSettings.Environment.ToLowerInvariant(), GlobalSettings.FI);
			OrgIdAPI.TryGetValue(_fiEnv, out string _orgId);
			CompanyNumberPLogAPI.TryGetValue(_fiEnv, out string _compNumPlog);
			BankNumberAPI.TryGetValue(_fiEnv, out string _bankNum);
			PasswordAPI.TryGetValue(_fiEnv, out string _pass);
			UsernameAPI.TryGetValue(_fiEnv, out string _uname);
			return new Tuple<string, string, string, string, string>(_orgId, _uname, _pass, _bankNum, _compNumPlog);
		}

        /// <summary>
        /// Tuple Data Set per FI for AP Service tests. This is static data in UAT.
        /// item 1 : OrgGroupLoginId
        /// item 2 : Username
        /// item 3 : Password
        /// item 4 : BankNumber
        /// item 5 : CompanyNumber
        /// </summary>
        public static Tuple<string, string, string, string, string> APIDataSet()
        {
			Tuple<string, string> _fiEnv = new Tuple<string, string>(GlobalSettings.Environment.ToLowerInvariant(), GlobalSettings.FI);
			OrgIdAPI.TryGetValue(_fiEnv, out string _orgId);
			CompanyNumberAPI.TryGetValue(_fiEnv, out string _compNum);
			BankNumberAPI.TryGetValue(_fiEnv, out string _bankNum);
			PasswordAPI.TryGetValue(_fiEnv, out string _pass);
			UsernameAPI.TryGetValue(_fiEnv, out string _uname);
			return new Tuple<string, string, string, string, string>(_orgId, _uname, _pass, _bankNum, _compNum);
        }
		
		/// </summary>
		/// All Usernames for each FI
		/// </summary>
		public static Dictionary<Tuple<string, string>, string> UsernameAPI { get; } = new Dictionary<Tuple<string, string>, string>()
		{
			{ new Tuple<string, string>("uat", FI.WEX.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.WEX.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "APITEST" },
            { new Tuple<string, string>("qaaws", FI.WEX.ToString()), "TestUser0" },

            { new Tuple<string, string>("uat", FI.AKUSA.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.AKUSA.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.PNC.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.REGIONS.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "APITEST1" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.USBANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "APITEST" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "APITEST" },
			{ new Tuple<string, string>("pat", FI.AMEX.ToString()), "APITEST" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "APITEST" }
		};

		/// </summary>
		/// All Passwords for each FI
		/// </summary>
		public static Dictionary<Tuple<string, string>, string> PasswordAPI { get; } = new Dictionary<Tuple<string, string>, string>()
		{
			{ new Tuple<string, string>("uat", FI.WEX.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.WEX.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "Testing2020!" },
            { new Tuple<string, string>("qaaws", FI.WEX.ToString()), "Test123!" },

            { new Tuple<string, string>("uat", FI.AKUSA.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.AKUSA.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "Testing123@" },
			{ new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "Testing123@" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "Testing123@" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.PNC.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.REGIONS.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.USBANK.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "Testing2020!" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "Testing2020#" },
			{ new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "Testing2020#" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "Testing2020#" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("pat", FI.AMEX.ToString()), "Testing2020!" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "Testing2020!" }
		};

		/// </summary>
		/// All Bank Numbers for each FI
		/// </summary>
		public static Dictionary<Tuple<string, string>, string> BankNumberAPI { get; } = new Dictionary<Tuple<string, string>, string>()
		{
			{ new Tuple<string, string>("uat", FI.WEX.ToString()), "2272" },
			{ new Tuple<string, string>("pat", FI.WEX.ToString()), "2272" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "7189" },
            { new Tuple<string, string>("qaaws", FI.WEX.ToString()), "0479" },

            { new Tuple<string, string>("uat", FI.AKUSA.ToString()), "7802" },
			{ new Tuple<string, string>("pat", FI.AKUSA.ToString()), "7802" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "7802" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "2748" },
			{ new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "2748" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "2748" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "BA" },
			{ new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "BA" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "BA" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "KB" },
			{ new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "KB" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "KB" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "1940" },
			{ new Tuple<string, string>("pat", FI.PNC.ToString()), "1940" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "1940" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "DG" },
			{ new Tuple<string, string>("pat", FI.REGIONS.ToString()), "DG" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "1038" },
			{ new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "1038" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "1038" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "3752" },
			{ new Tuple<string, string>("pat", FI.USBANK.ToString()), "3752" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "3752" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "IB" },
			{ new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "IB" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "IB" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "AMEX" },
			{ new Tuple<string, string>("pat", FI.AMEX.ToString()), "AMEX" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "AMEX" }
		};
		
		/// </summary>
		/// All Org Ids for each FI
		/// </summary>
		public static Dictionary<Tuple<string, string>, string> OrgIdAPI { get; } = new Dictionary<Tuple<string, string>, string>()
		{
			{ new Tuple<string, string>("uat", FI.WEX.ToString()), "APITEST_WEX" },
			{ new Tuple<string, string>("pat", FI.WEX.ToString()), "APITEST_WEX" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "EncQATest01" },
            { new Tuple<string, string>("qaaws", FI.WEX.ToString()), "dgTest" },

            { new Tuple<string, string>("uat", FI.AKUSA.ToString()), "APITEST_AK" },
			{ new Tuple<string, string>("pat", FI.AKUSA.ToString()), "APITEST_AK" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "SFTEST" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "APITEST_CBKC" },
			{ new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "APITEST_CBKC" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "APTEST" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "APITEST_Central" },
			{ new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "APITEST_Central" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "Encompass QA" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "APITEST_KEY" },
			{ new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "APITEST_KEY" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "AOCTESTORG AP" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "APITEST_PNC" },
			{ new Tuple<string, string>("pat", FI.PNC.ToString()), "APITEST_PNC" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "ACTIVEPAYTEST33333" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "5311491568524206" },
			{ new Tuple<string, string>("pat", FI.REGIONS.ToString()), "" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "APITEST_SYN" },
			{ new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "APITEST_SYN" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "CORPBILL" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "APITEST_USB" },
			{ new Tuple<string, string>("pat", FI.USBANK.ToString()), "APITEST_USB" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "AOC_3752" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "APITEST_IB" },
			{ new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "APITEST_IB" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "AOC_AP" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "APITEST_AMEX" },
			{ new Tuple<string, string>("pat", FI.AMEX.ToString()), "APITEST_AMEX" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "002_LiveOrg" }
		};
		
		/// </summary>
		/// All Account # with Auths for each FI
		/// </summary>
		public static Dictionary<Tuple<string, string>, string> AuthAccountNumbers { get; } = new Dictionary<Tuple<string, string>, string>()
        {
            { new Tuple<string, string>("uat", FI.WEX.ToString()), "5109564932202208" },
            { new Tuple<string, string>("pat", FI.WEX.ToString()), "5100978033682804" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.AKUSA.ToString()), "5177476190931880" },
            { new Tuple<string, string>("pat", FI.AKUSA.ToString()), "5577339062637355" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "5287208335880491" },
            { new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "5109678612127202" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "5433671897470444" },
            { new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "5111120668412408" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "5423633129079437" },
            { new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "5294374039647866" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "5583564871814943" },
            { new Tuple<string, string>("pat", FI.PNC.ToString()), "5466949017925335" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "5311491568524206" },
            { new Tuple<string, string>("pat", FI.REGIONS.ToString()), "" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "5175467231527706" },
            { new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "5537275125902023" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "5393887023519916" },
            { new Tuple<string, string>("pat", FI.USBANK.ToString()), "5330052993779596" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "5310847739922705" },
            { new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "5117890172371001" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "343921550417574" },
            { new Tuple<string, string>("pat", FI.AMEX.ToString()), "348566992131799" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "" }
		};

		/// <summary>
		/// SUGA Account numbers to be used for API tests in PROD
		/// </summary>
		public static Dictionary<string, string> ProdSUGAAccountsAPI { get; } = new Dictionary<string, string>()
		{
			{ FI.AMEX.ToString(), "370021056474503" },
			{ FI.USBANK.ToString(), "4288012559883814" },
			{ FI.CENTRAL.ToString(), "5563740152630827" },
			{ FI.PNC.ToString(), "4715150026728122" },
			{ FI.COMMERCE.ToString(), "4485007091967670" },
			{ FI.WEX.ToString(), "5552320044425299" },
			{ FI.KEYBANK.ToString(), "5569887156509558" }
		};

        /// <summary>
        /// All Company Numbers for each FI
        /// </summary>
        public static Dictionary<Tuple<string, string>, string> CompanyNumberAPI { get; } = new Dictionary<Tuple<string, string>, string>()
        {
            { new Tuple<string, string>("uat", FI.WEX.ToString()), "51035" },
            { new Tuple<string, string>("pat", FI.WEX.ToString()), "66334" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "9999113" },
            { new Tuple<string, string>("qaaws", FI.WEX.ToString()), "49488" },

            { new Tuple<string, string>("uat", FI.AKUSA.ToString()), "44298" },
            { new Tuple<string, string>("pat", FI.AKUSA.ToString()), "98640" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "90068" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "2173144" },
            { new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "8111337" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "0605706" },

			{ new Tuple<string, string>("uat", FI.CENTRAL.ToString()), "3743299" },
            { new Tuple<string, string>("pat", FI.CENTRAL.ToString()), "5757866" },
			{ new Tuple<string, string>("prod", FI.CENTRAL.ToString()), "9999801" },

			{ new Tuple<string, string>("uat", FI.KEYBANK.ToString()), "3678461" },
            { new Tuple<string, string>("pat", FI.KEYBANK.ToString()), "2729542" },
			{ new Tuple<string, string>("prod", FI.KEYBANK.ToString()), "9999902" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "66701" },
            { new Tuple<string, string>("pat", FI.PNC.ToString()), "80917" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "33333" },

			{ new Tuple<string, string>("uat", FI.REGIONS.ToString()), "3567810" },
            { new Tuple<string, string>("pat", FI.REGIONS.ToString()), "" },
			{ new Tuple<string, string>("prod", FI.REGIONS.ToString()), "" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "58180" },
            { new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "93826" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "22565" },

			{ new Tuple<string, string>("uat", FI.USBANK.ToString()), "34265" },
            { new Tuple<string, string>("pat", FI.USBANK.ToString()), "23022" },
			{ new Tuple<string, string>("prod", FI.USBANK.ToString()), "10044" },

			{ new Tuple<string, string>("uat", FI.IBERIABANK.ToString()), "9289578" },
            { new Tuple<string, string>("pat", FI.IBERIABANK.ToString()), "7515148" },
			{ new Tuple<string, string>("prod", FI.IBERIABANK.ToString()), "8888801" },

			{ new Tuple<string, string>("uat", FI.AMEX.ToString()), "1116611" },
            { new Tuple<string, string>("pat", FI.AMEX.ToString()), "6202549" },
			{ new Tuple<string, string>("prod", FI.AMEX.ToString()), "1000089" }
		};

        /// <summary>
        /// All Company Numbers for each FI
        /// </summary>
        public static Dictionary<Tuple<string, string>, string> CompanyNumberPLogAPI { get; } = new Dictionary<Tuple<string, string>, string>()
        {
            { new Tuple<string, string>("uat", FI.WEX.ToString()), "51035" },
            { new Tuple<string, string>("pat", FI.WEX.ToString()), "66334" },
			{ new Tuple<string, string>("prod", FI.WEX.ToString()), "9999113" },

			{ new Tuple<string, string>("uat", FI.AKUSA.ToString()), "44298" },
            { new Tuple<string, string>("pat", FI.AKUSA.ToString()), "98640" },
			{ new Tuple<string, string>("prod", FI.AKUSA.ToString()), "90068" },

			{ new Tuple<string, string>("uat", FI.COMMERCE.ToString()), "7997083" },
            { new Tuple<string, string>("pat", FI.COMMERCE.ToString()), "8111337" },
			{ new Tuple<string, string>("prod", FI.COMMERCE.ToString()), "0605706" },

			{ new Tuple<string, string>("uat", FI.PNC.ToString()), "66701" },
            { new Tuple<string, string>("pat", FI.PNC.ToString()), "80917" },
			{ new Tuple<string, string>("prod", FI.PNC.ToString()), "33333" },

			{ new Tuple<string, string>("uat", FI.SYNOVUS.ToString()), "58180" },
            { new Tuple<string, string>("pat", FI.SYNOVUS.ToString()), "93826" },
			{ new Tuple<string, string>("prod", FI.SYNOVUS.ToString()), "22565" }
		};

		/// <summary>
		/// PRod Merchant Codes to be used in APIs
		/// </summary>
		public static Dictionary<string, string> ProdMerchantCodesAPI{ get; } = new Dictionary<string, string>()
		{
			{ FI.WEX.ToString(), "SUGA" },
			{ FI.AKUSA.ToString(), "ProcMe1" },
			{ FI.AMEX.ToString(), "Merchant Test One" },
			{ FI.CENTRAL.ToString(), "test1032" },
			{ FI.COMMERCE.ToString(), "procme1" },
			{ FI.IBERIABANK.ToString(), "SUGA Merchant" },
			{ FI.KEYBANK.ToString(), "073116_VendorPlus" },
			{ FI.PNC.ToString(), "ProcMe1" },
			{ FI.REGIONS.ToString(), "" },
			{ FI.SYNOVUS.ToString(), "Ranorex Automated Tests" },
			{ FI.USBANK.ToString(), "DSMer1" },
		};			

		#endregion

		/// <summary>
		/// List of all Profiles options
		/// </summary>
		public static List<string> ProfilesOptions
        {
            get
            {
                return new List<string>()
                {
                    "Save",
                    "Save As",
                    "Set Default",
                    "Reset"
                };
            }
        }

        /// <summary>
        /// FIs which allow Monthly Credit Limit.
        /// </summary>
        public static List<string> FIsWithMonthlyLimitEnabled
        {
            get
            {
                return new List<string>() {
                    FI.WEX.ToString(),
                    FI.AKUSA.ToString(),
                    FI.KEYBANK.ToString(),
                    FI.PNC.ToString(),
                    FI.SYNOVUS.ToString(),
                    FI.USBANK.ToString(),
                    FI.AMEX.ToString()
                };
            }
        }

        #region MQLive Testing Data

        /// <summary>
		/// Set of Compno and Bank No for given FI to be used for MQlive orgs
		/// </summary>
		public static Tuple<string, string> MQLiveCompBankNos(string fi) 
        {           
            switch (fi.ToLowerInvariant())
			{
				case "keybank": return new Tuple<string, string>("9999902", "KB");
				case "wex": return new Tuple<string, string>("10450", "6472");
                case "regions": return new Tuple<string, string>("1299901", "N/A"); // Bank no unknown
                case "central": return new Tuple<string, string>("9999801", "BA");
				case "iberiabank": return new Tuple<string, string>("1077303", "IB");
				case "commerce":return new Tuple<string, string>("0605706", "2748");
                case "synovus": return new Tuple<string, string>("22565", "1038");
                case "pnc": return new Tuple<string, string>("33333", "1940");
                case "usbank":	return new Tuple<string, string>("10044", "3752");
                case "akusa": return new Tuple<string, string>("90068", "7802");
                case "amex": return new Tuple<string, string>("1000271", "AMEX");
                default: return null;
            }
        }

		/// <summary>
		/// List all the fields to modify in Edit Card page for each FI
		/// </summary>
		public static List<String> MQLiveFieldsToEdit(string fiName)
		{
			List<string> fieldsToEdit = new List<string>();
			switch (fiName.ToLowerInvariant())
			{							
				case "central":
					fieldsToEdit.Add("FirstName");
					fieldsToEdit.Add("LastName");
					fieldsToEdit.Add("EmployeeId");
					fieldsToEdit.Add("BusinessPhone");
					fieldsToEdit.Add("SSN");
					fieldsToEdit.Add("SinglePurCdtLimit");
					fieldsToEdit.Add("CreditLimit");
					break;
				case "pnc":
					fieldsToEdit.Add("FirstName");
					fieldsToEdit.Add("LastName");
					fieldsToEdit.Add("EmployeeId");
					fieldsToEdit.Add("BusinessPhone");
					fieldsToEdit.Add("SSN");
					fieldsToEdit.Add("SinglePurCdtLimit");
					fieldsToEdit.Add("CreditLimit");
					break;
				case "akusa":
					fieldsToEdit.Add("FirstName");
					fieldsToEdit.Add("LastName");
					fieldsToEdit.Add("EmployeeId");
					fieldsToEdit.Add("BusinessPhone");
					fieldsToEdit.Add("CreditLimit");
					break;
				case "amex":
					fieldsToEdit.Add("SinglePurCdtLimit");
					fieldsToEdit.Add("CreditLimit");
					break;
                case "synovus":
                    fieldsToEdit.Add("CreditLimit");
                    break;
                default:
					fieldsToEdit.Add("FirstName");
					fieldsToEdit.Add("LastName");
					fieldsToEdit.Add("EmployeeId");
					fieldsToEdit.Add("BusinessPhone");
					fieldsToEdit.Add("SSN");
					fieldsToEdit.Add("SinglePurCdtLimit");
					fieldsToEdit.Add("CreditLimit");
					fieldsToEdit.Add("TmpCdtLtStartDt");
					fieldsToEdit.Add("TmpCdtLtEndDt");
					fieldsToEdit.Add("TmpCdtLimit");
					fieldsToEdit.Add("TmpSinglePurLtStDt");
					fieldsToEdit.Add("TmpSinglePurLtEndDt");
					fieldsToEdit.Add("TmpSinglePurLt");
					break;
			}
			return fieldsToEdit;
		}     

        #endregion
    }
}
