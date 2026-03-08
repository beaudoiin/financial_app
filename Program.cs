using ClosedXML.Excel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using static Assignment1.Program;
namespace Assignment1 {
    #region Verious Enums for controlling program flow, menu states, and other features. Using enums allows us to limit options to compile time constants and makes code more readable.
    /// <summary>
    /// Used to look up messages in the dictionary. This is intentionally a mix of snake case and pascal to aid in xml comments and readability of code.
    /// </summary>
    enum MessageEnum {
        #region >>> // System
        System_AnyKeyToContinue,
        System_AnyKeyToExit,
        System_YToQuitProgram,
        System_NoReleventTransactions,
        #endregion
        #region >>> // System instructions
        SystemInstructions_AnyKeyToAck,
        SystemInstructions_EscapeOrBackspace,
        SystemInstructions_ToExitOrAbort,
        SystemInstructions_InputYearForSummary,
        SystemInstructions_InputMonthForSummary,
        SystemInstructions_Abort,
        SystemInstructions_InputIncomeAmount,
        SystemInstructions_InputTransDescription,
        SystemInstructions_EnterDate,
        SystemInstructions_ToSkip,
        SystemInstructions_SpaceOrEnter,
        SystemInstructions_ToLoad,
        SystemInstructions_PageView,
        SystemInstructions_PressToExit,

        #endregion
        #region >>> // Warning
        Warning_BadDate,
        Warning_BadInput,
        Warning_DateFormat,
        Warning_DateFormatYYYY,
        Warning_EmptyOrSpaces,
        Warning_BadAmountNoZero,
        Warning_BadAmountZeroOk,
        Warning_InvalidYearOld,
        Warning_InvalidYearNew,
        Warning_InvalidMonth,
        Warning_YearTooBig,
        Warning_YearInFuture,
        Warning_SameDates,
        Warning_CultureNotFound,
        Warning_LanguageNotInList,
        Warning_XmlFormat,
        Warning_FileNotAuthorized,
        Warning_ArgumentIssue,
        Warning_FileNotFound,
        Warning_DirectoriesNotFound,
        Warning_FileNull,
        Warning_GeneralException,
        Warning_NoTransactionsOrBudgetFound,
        Warning_DeleteTransactions,

        #endregion
        #region >>> // Labels
        Label_Exit,
        Label_Enter,
        Label_Press,
        Label_toTryAgain,
        Label_LoadFile,
        Label_Skip,
        Label_Or,
        Label_And,
        Label_To,
        Label_Yes,
        Label_No,
        Label_Aborted,
        Label_TransactionAborted,
        Label_SummaryAborted,
        Label_SearchAborted,
        Label_Starting,
        Label_Options,
        Label_Date,
        Label_All,
        Label_Yearly,
        Label_Years,
        Label_Monthly,
        Label_Amount,
        Label_Description,
        Label_AddIncomeTransaction,
        Label_Income,
        Label_Total,
        Label_Balance,
        Label_AddExpenseTransaction,
        Label_Expense,
        Label_Category,
        Label_Instructions,
        Label_PageAbreviated,
        Label_TotalIncome,
        Label_TotalExpenses,
        Label_TotalBalances,
        Label_HighestExpenseCategory,
        Label_AtCostOf,
        Label_FileName,
        Label_Attempt,
        Label_SForSecond,
        Label_FileFound,
        Label_Found,

        #endregion
        #region >>> // General menu options
        Menu_Return,
        Menu_HeaderOuterDecor,
        #endregion
        #region >>> // Main Menu options
        MainMenu_Header,
        MainMenu_TransactionManagement,
        MainMenu_BudgetTools,
        MainMenu_ReportsAndSummary,
        MainMenu_Load,
        MainMenu_Save,
        MainMenu_Options,
        #endregion
        #region >>> // Transaction management options
        TransMgnt_AddIncomeTransaction,
        TransMgnt_AddExpenseTransaction,
        TransMgnt_ViewAllTransactions,
        TransMgnt_SearchTransactions,
        TransMgnt_TransactionAdded,
        TransMgnt_AddingExpenseFor,
        TransMgnt_NoDscProvided,
        TransMgnt_LabelTransCategory,


        #endregion
        #region >>> // Search by transactions
        SrcByTrans_HeaderQuestion,
        SrcByTrans_DateRange,
        SrcByTrans_PriceRange,
        SrcByTrans_Category,
        SrcByTrans_OptionOrderAsc,
        SrcByTrans_OptionOrderDesc,
        SrcByTrans_OptionTableColorBanding,
        SrcByTrans_OptionApplied,
        SrcByTrans_NoResultSrcAgain,
        SrcByTrans_SrcAborted,
        SrcByTrans_EnterDate1,
        SrcByTrans_FirstDateIs,
        SrcByTrans_EnterDate2,
        SrcByTrans_EnterAmount1,
        SrcByTrans_FirstAmountIs,
        SrcByTrans_EnterAmount2,

        #endregion
        #region >>> // Budget tools options 
        BudgetMenu_Header,
        BudgetMenu_SetMonthlyBudget,
        BudgetMenu_UpdateBudgetCateg,
        BudgetMenu_CheckRemainBudget,
        BudgetMenu_Warning80PercentOverBudget,
        BudgetMenu_CurrentBalance,
        BudgetMenu_SelectionInstruction,
        BudgetMenu_UpdateInstruction,
        BudgetMenu_WarningInstruction,
        BudgetMenu_AmountExceeded,
        BudgetMenu_AmountAccepted,
        BudgetMenu_NotUpdated,
        BudgetMenu_Updated,
        BudgetMenu_AmountInvalid,
        BudgetMenu_BudgetExceeded,
        #endregion
        #region >>> // Reports & Summary options
        ReportAndSum_AccountOverview,
        ReportAndSum_YearlySummary,
        ReportAndSum_MonthlySummary,
        ReportAndSum_SaveExcel,
        ReportAndSum_TotalIncome,
        ReportAndSum_TotalExpense,
        ReportAndSum_HighestExpenseCategory,
        ReportAndSum_AskHowToView,
        ReportAndSum_Scroll,
        ReportAndSum_Pages,
        ReportAndSum_PageAndScrollNoClear,
        ReportAndSum_NoTRansactionsInYear,
        ReportAndSum_AccountSummaryFrom,
        ReportAndSum_NoTRansactionsInMonth,
        ReportAndSum_AcountSummary,
        Excel_WorksheetNotFound,
        Excel_WelcomeMessage,
        Excel_BankRecommendations,
        Excel_BankRec1,
        Excel_BankRec2,
        Excel_BankRec3,
        Excel_BankRec4,
        Excel_BankRec5,
        Excel_BankRec6,
        Excel_BankRec7,
        Excel_EmptyMonth,
        Excel_SavedMessage,
        Excel_FileNoAccessMessage,
        #endregion
        #region >>> // Data options
        DataOptions_Header,
        DataOptions_LoadFile,
        DataOptions_LoadSample,
        DataOptions_NoloadOrSamples,
        DataOptions_WarningSavingWithNoDataMayOverwrite,
        DataOptions_DeleteTransactions,
        DataOptions_TransactionsDeleted,
        DataOptions_PrintTransactionCount,
        DataOptions_LabelAmountOfTrans,
        DataOptions_WarningThisPrintsOnlyRam,
        #endregion
        #region >>> // Menu Options
        Options_ChangLang,
        Options_AutoSave,
        #endregion
        #region >> // Get Pwd
        GetPwd_Header,
        GetPwd_PwSafteyReminder,
        GetPwd_SecurePwIsHeader,
        GetPwd_Instruction15Chars,
        GetPwd_InstructionContainDigit,
        GetPwd_InstructionSpecialChar,
        GetPwd_InstructionMixCase,
        GetPwd_EnterPw,
        GetPwd_ConfirmPw,
        GetPwd_Warning_PwDontMatch,
        GetPwd_Warning_PwDontMeetCriteria,
        GetPwd_Warning_OverOneTrillionWarning,
        GetPwd_Warning_TooManyAttempts,
        GetPwd_EnterPwTransactionFile,
        GetPwd_Warning_CoolDown,
        GetPwd_Warning_AbortWithNoTransactions,

        #endregion
        #region >>> // GetAmount, GetCategory, GetDate

        GetCategory_ChooseCategory,
        GetCategory_InstructionHowMakeChoice,
        GetDate_SameDates,
        GetDate_SearchingDatesBetween,

        #endregion
        #region >>> // ChooseLang
        ChooseLang_Header,
        ChooseLang_RevertingToEng,
        ChooseLang_LangApplied,

        #endregion

        #region >> //LoadFile
        LoadFile_AbordTransactionFileFound, //This is found in load file after cooldown when user escapes
        LoadFile_TooManyIncorrect,
        LoadFile_EnterPwForTransactionFile,
        LoadFile_IncorrectPwCoolDown,
        LoadFile_ToAbortStartNoTrans,
        LoadFile_NoTransactionsFound,
        LoadFile_ConfirmLoadingBudgetFileOnly,
        LoadFile_NoFileFound,
        LoadFile_BudgetFileFound,
        LoadFile_ConfrimLoadBudgetFile,
        LoadFile_NoTransOrBudgetFileFound,
        LoadFile_PwIncorrect,
        LoadFile_ForOtherOptionsSampleData,
        LoadFile_ConfrimTryAnotherPw,
        LoadFile_TooManyWrongPwAttempts,
        LoadFile_CooldownForNextAttempt,
        #region >> // SampleTransaction
        Sample_Header,
        Sample_Loaded,
        #endregion

        #endregion
        #region >> // Write Trans and Budget
        Write_SkipSaving,
        Write_Saved,
        #endregion
        #region >> // Budget Categories
        Category_Income,
        Category_Housing,
        Category_Groceries,
        Category_Transportation,
        Category_Utilities,
        Category_Restaurants,
        Category_Insurance,
        Category_Debt,
        Category_Entertainment,
        Category_Healthcare,
        Category_Transfers,
        Category_Fees,
        Category_Other
        #endregion
    }

    /// <summary>
    /// Keeps track of what message is printed on the budget screen for guiding user input.
    /// </summary>
    enum MessageFlagEnum {
        NoMessage,
        ExceedingInputSize,
        BudgetUpdated,
        BudgetNotUpdated,
        InvalidNumber,
        AcceptedNumber
    }

    /// <summary>
    /// Used to specify which date formating to use. Limits options to compile time constants
    /// </summary>
    enum DateFormatEnum {
        NumberMonth,
        ShortMonth,
        LONGMonth
    }

    /// <summary>
    /// Enum for pointing to colors based on groups in the dictionary colorByGroup.
    /// </summary>
    enum ColorGroup {
        Default,
        SystemWarning,
        SystemError,
        SystemInstructions,
        SystemInstructionsGray,
        SystemPromptHint,
        SystemPromptInstructions,
        MenuHeadings,
        MenuItems,
        Success,
        Header,
        InputStyleA,
        InputStyleText
    }

    /// <summary>
    /// Enum to specify how transactions are displayed in lists. Pages, Giant List, or running history of pages (scroll up not using Console.Clear())
    /// </summary>
    enum ScrollType {
        Pager,
        Scroller,
        Both
    }

    /// <summary>
    /// Specify which option the user can choose for searching transactions.
    /// </summary>
    enum SearchType {
        DateRange,
        PriceRange,
        Category
    }

    /// <summary>
    /// Specifies the level of detail for summarizing data, such as aggregating all data, grouping by year, or grouping
    /// by month.
    /// </summary>
    /// <remarks>Use this enumeration to select the desired granularity when generating summary reports or
    /// statistics. Each value represents a distinct time frame for data aggregation, allowing flexibility in how
    /// results are presented.</remarks>
    enum SummaryType {
        All,
        Year,
        Month
    }
    #endregion

    #region >>> // Transaction Category enum
    /// <summary>
    /// Holds each transaction type and is crucial for both formatting and logic.
    /// You can add onto this list other transasctions, however if you increase past the Console WindowHeight you
    /// may need to adjust the code to print in columns or other features. This is a decent standard list.
    /// Income needs to be 0, it seperates it from transactions.
    /// </summary>
    public enum TransactionCategory {
        Income = 0, //Keep the 0 incase you move this it will always be first in the list.
        Housing,
        Groceries,
        Transportation,
        Utilities,
        Restaurants,
        Insurance,
        Debt,
        Entertainment,
        Healthcare,
        Transfers,
        Fees,
        Other
    }
    #endregion
    class Program {
        #region >>> // Language Related
        //Laungauge file
        const string langFile = """
            <?xml version="1.0" encoding="utf-8"?>
            <!-- Add quickly using Regex (<..>)|(Warning_BadAmountNoZero) where th elatter is the tagname you want to find. insert new tag after it as refrence-->
            <lang>
            	<zh>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>金额已接受</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>金额不能超过</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>金额无效</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>预算已超出</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>检查剩余预算</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>查看当前余额</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>预算菜单</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>预算未更新！</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>按上方菜单键，输入新金额并按 Enter 更新。</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>设置每月预算</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>更新预算类别</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>预算已更新！</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>当框为空时表示处于编辑模式。</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>警告！您已使用超过预算的 80%</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>当预算超过 80% 或 100% 时会显示警告。</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>语言选择</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>语言已应用！</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>恢复为默认英语词典</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>删除所有交易</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>数据选择</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>当前交易数量</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>从磁盘加载</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>加载示例数据</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>无交易启动</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>显示交易数量</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>所有交易已删除</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>警告：加载示例数据并修改后可能会覆盖您的交易文件。仅用于测试。</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>该数量仅统计内存中的数据。</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>根据您的收入，我们可以提供年收益 7% 的高收益 RRSP 投资方案。</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>根据您的收入，我们可以提供年收益 4% 的中等收益 RRSP 投资方案。</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>根据您的收入，我们建议开设储蓄账户以建立紧急资金。</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>根据您的收入，我们可以提供年利率 22% 且首月免息的信用卡。</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>根据您的收入，我们可以提供年利率 12% 且三个月免息的信用卡。</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>根据您的收入，我们建议进行免费的信用咨询并提高收入稳定性。</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>您的财务状况似乎较为严重，请尽快前往银行与我们讨论解决方案。</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>银行建议</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>该月份没有交易记录，此文件仅供存档。</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>无法访问该文件，请确保文件未被其他程序打开并且应用程序具有写入权限。</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Excel 文件已保存到程序所在文件夹。</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>感谢使用本程序。希望您喜欢这份账户摘要。其他工作表中包含最近 12 个月的详细信息。</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>未找到工作表！</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>选择交易类别</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>按对应的键进行选择</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>两个日期相同，请输入不同的日期</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>正在搜索日期范围</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>确认文件密码</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>输入文件密码</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>输入安全文件密码</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>至少 15 个字符，</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>包含数字</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>包含大小写字母</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>包含至少一个特殊字符</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>请记住，此密码不会保存在电脑上。如果遗忘，将无法访问您的交易数据库！</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>安全密码要求：</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>单笔交易金额不能超过一万亿，请拆分为多个交易。</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>两次输入的密码不一致！</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>密码不符合要求。</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>已取消</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>添加支出交易</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>添加收入交易</message>
            		</item>
            		<item key="Label_All">
            			<message>全部</message>
            		</item>
            		<item key="Label_Amount">
            			<message>金额</message>
            		</item>
            		<item key="Label_And">
            			<message>和</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>花费为</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>尝试</message>
            		</item>
            		<item key="Label_Balance">
            			<message>余额</message>
            		</item>
            		<item key="Label_Category">
            			<message>类别</message>
            		</item>
            		<item key="Label_Date">
            			<message>日期</message>
            		</item>
            		<item key="Label_Description">
            			<message>描述</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>支出</message>
            		</item>
            		<item key="Label_FileName">
            			<message>文件名</message>
            		</item>
            		<item key="Label_Found">
            			<message>找到</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>最高支出类别</message>
            		</item>
            		<item key="Label_Income">
            			<message>收入</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>说明</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>每月</message>
            		</item>
            		<item key="Label_No">
            			<message>否</message>
            		</item>
            		<item key="Label_Options">
            			<message>选项</message>
            		</item>
            		<item key="Label_Or">
            			<message>或</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>页</message>
            		</item>
            		<item key="Label_Press">
            			<message>按</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>搜索已取消</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>秒</message>
            		</item>
            		<item key="Label_Starting">
            			<message>正在启动</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>摘要已取消</message>
            		</item>
            		<item key="Label_To">
            			<message>到</message>
            		</item>
            		<item key="Label_Total">
            			<message>总计</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>总余额</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>总支出</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>总收入</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>重试</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>交易已取消</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>每年</message>
            		</item>
            		<item key="Label_Years">
            			<message>年</message>
            		</item>
            		<item key="Label_Yes">
            			<message>是</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>仍然加载找到的预算文件吗？</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>是否尝试其他密码？</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>下一次尝试前需要等待冷却时间</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>请输入密码</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>用于其他选项（例如示例数据）</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>密码错误冷却时间</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>在程序目录中未找到文件！</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>密码与文件不匹配！</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>无交易启动</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>密码错误次数过多。</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>密码错误次数过多</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>预算工具</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>主菜单</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>加载交易文件</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>选项</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>报告与摘要</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>保存交易文件</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>交易管理</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>返回主菜单</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>每次更改后自动保存（较慢）</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>更改语言</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>账户概览</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>账户摘要从</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>账户摘要</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>您想如何查看报告？</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>最高支出类别</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>月度摘要</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>该月份没有交易，无法显示摘要。</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>该年份没有交易，无法显示摘要。</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>分页视图（不清屏）</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>分页视图</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>导出账户摘要和 12 个月摘要到 Excel 文件</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>列表视图</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>总支出</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>总收入</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>年度摘要</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>正在加载示例交易数据...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>示例交易数据已加载...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>按类别</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>按日期范围</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>请输入范围中的第一个日期。</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>请输入范围中的第二个日期。</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>第一个日期是</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>您想如何搜索交易？</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>未找到交易，请使用不同参数重新搜索。</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>选项已应用</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>按日期升序排序</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>按日期降序排序</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>表格颜色分隔以便阅读</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>按金额范围</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>搜索已取消</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>按任意键继续</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>按任意键退出</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>没有相关交易，无法查看。</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>确定要退出吗？按 (Y) 退出，按其他任意键继续</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>按键退出</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>输入 exit 以取消</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>按任意键确认</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>请按以下格式输入交易日期：</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Esc 或 Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>请输入一个正数作为收入金额</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>选择要查看摘要的月份</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>请输入交易描述</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>选择要查看摘要的年份</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>上一页: ↑ ← PgUp | 下一页: ↓ → PgDn | 退出: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>空格 或 Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>用于退出/取消</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>用于加载</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>用于跳过</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>添加支出交易</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>添加收入交易</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>正在添加支出：</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>交易类别</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>未提供描述</message>
            		</item>
            		<item key="TransMgnt_SearchTransactions">
            			<message>搜索交易</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>交易已成功添加！</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>查看所有交易</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>传入的文件格式不符合预期！</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>金额必须大于零且不能为空。</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>金额必须大于或等于零，并且不能为空。</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>请使用正确的日期格式</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>输入无效！请重试！</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>未找到对应的区域设置：</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>删除交易将清除当前数据并在保存时覆盖文件。此操作不可恢复，请在需要时备份您的交易文件。</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>未找到目录！</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>不能为空或仅包含空格！</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>没有访问该文件的权限！</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>未找到文件！</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>空引用异常！</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>加载文件时发生错误！</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>月份必须为 1 到 12 的数字</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>请确保日期不早于允许的最早日期</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>日期不能是未来的日期。</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>该编号不在列表中！</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>未找到交易或预算文件</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>XML 格式不正确！</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>正在保存到磁盘...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>没有交易可保存，跳过保存...</message>
            		</item>
            		<item key="Category_Income">
            			<message>收入</message>
            		</item>

            		<item key="Category_Housing">
            			<message>住房</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>杂货</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>交通</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>水电费</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>餐厅</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>保险</message>
            		</item>

            		<item key="Category_Debt">
            			<message>债务</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>娱乐</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>医疗保健</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>转账</message>
            		</item>

            		<item key="Category_Fees">
            			<message>费用</message>
            		</item>

            		<item key="Category_Other">
            			<message>其他</message>
            		</item>
            	</zh>
            	<ru>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>Сумма принята</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>Сумма не может превышать</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>Недопустимая сумма</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>Бюджет превышен</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>Проверить оставшийся бюджет</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>Проверить текущий баланс</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>Меню бюджета</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>Бюджет не обновлён!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>Нажмите клавишу меню выше, введите новую сумму и нажмите Enter для обновления.</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>Установить месячный бюджет</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>Обновить категорию бюджета</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>Бюджет обновлён!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>Пустое поле означает режим редактирования.</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>Предупреждение! Вы превысили 80% бюджета</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>Предупреждения отображаются при превышении 80% или 100% бюджета</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>Выбор языка</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>Язык применён!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>Возврат к словарю английского языка по умолчанию</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>Удалить все транзакции</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>Выбор данных</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>Количество сохранённых транзакций</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>Загрузить с диска</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>Загрузить пример данных</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>Начать без транзакций</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>Показать количество транзакций</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>Все транзакции удалены</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>Предупреждение: загрузка тестовых данных и изменение бюджета может перезаписать файл транзакций.</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>Отображается только количество транзакций в памяти.</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>На основе вашего дохода мы можем предложить высокодоходный управляемый RRSP с гарантией 7% годовых.</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>На основе вашего дохода мы можем предложить управляемый RRSP со средней доходностью и гарантией 4% годовых.</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>На основе вашего дохода мы рекомендуем открыть сберегательный счёт для создания резервного фонда.</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>На основе вашего дохода мы можем предложить кредитную карту с 22% APR и без процентов в первый месяц.</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>На основе вашего дохода мы можем предложить кредитную карту с 12% APR и тремя месяцами без процентов.</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>Мы рекомендуем бесплатную консультацию по кредитам и сосредоточиться на повышении стабильности дохода.</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>Ваше финансовое положение выглядит критическим. Пожалуйста, посетите наш офис как можно скорее.</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>Рекомендации банка</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>В этом месяце нет транзакций. Документ предоставлен для ваших записей.</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>Не удалось получить доступ к файлу. Убедитесь, что он не открыт в другой программе и приложение имеет разрешение на запись.</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Файл Excel сохранён в папке программы.</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>Спасибо за использование программы. Ниже краткая сводка вашего счета. Подробности доступны на других листах, содержащих данные за последние 12 месяцев.</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>Лист не найден!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>Выберите категорию транзакции</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>Нажмите соответствующую клавишу, чтобы сделать выбор</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>Эти даты совпадают! Введите другую дату</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>Поиск дат между</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>Подтвердите пароль</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>Введите пароль для ваших файлов</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>Введите пароль для защищённых файлов</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>Содержать не менее 15 символов,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>Содержать цифру</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>Содержать буквы в верхнем и нижнем регистре</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>Содержать хотя бы один специальный символ</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>Помните: пароль не хранится на компьютере. Если вы его забудете, доступ к базе транзакций будет потерян.</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>Надёжный пароль должен:</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>Одна транзакция не может превышать один триллион. Разделите её на несколько.</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>Пароли не совпадают!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>Пароль не соответствует требованиям.</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>Отменено</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>Добавить расходную транзакцию</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>Добавить доходную транзакцию</message>
            		</item>
            		<item key="Label_All">
            			<message>Все</message>
            		</item>
            		<item key="Label_Amount">
            			<message>Сумма</message>
            		</item>
            		<item key="Label_And">
            			<message>и</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>стоимостью</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>Попытка</message>
            		</item>
            		<item key="Label_Balance">
            			<message>Баланс</message>
            		</item>
            		<item key="Label_Category">
            			<message>Категория</message>
            		</item>
            		<item key="Label_Date">
            			<message>Дата</message>
            		</item>
            		<item key="Label_Description">
            			<message>Описание</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>Расход</message>
            		</item>
            		<item key="Label_FileName">
            			<message>Имя файла</message>
            		</item>
            		<item key="Label_Found">
            			<message>Найдено</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>Категория с наибольшими расходами</message>
            		</item>
            		<item key="Label_Income">
            			<message>Доход</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>Инструкции</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>Месячный</message>
            		</item>
            		<item key="Label_No">
            			<message>Нет</message>
            		</item>
            		<item key="Label_Options">
            			<message>Опции</message>
            		</item>
            		<item key="Label_Or">
            			<message>или</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>стр.</message>
            		</item>
            		<item key="Label_Press">
            			<message>Нажмите</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>Поиск отменён</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>с</message>
            		</item>
            		<item key="Label_Starting">
            			<message>Запуск...</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>Сводка отменена</message>
            		</item>
            		<item key="Label_To">
            			<message>до</message>
            		</item>
            		<item key="Label_Total">
            			<message>Итого</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>Общий баланс</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>Общие расходы</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>Общий доход</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>Чтобы попробовать снова</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>Транзакция отменена</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>Годовой</message>
            		</item>
            		<item key="Label_Years">
            			<message>Годы</message>
            		</item>
            		<item key="Label_Yes">
            			<message>Да</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>Загрузить найденный файл бюджета?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>Попробовать другой пароль?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>Перед следующей попыткой будет пауза.</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>Введите пароль</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>для других вариантов (например тестовые данные)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>Ожидание после неправильного пароля</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>Файл не найден в каталоге программы!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>Пароль не соответствует файлу!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>Запуск без транзакций</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>Слишком много неправильных попыток ввода пароля.</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>Слишком много неправильных попыток пароля</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>Инструменты бюджета</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>Главное меню</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>Загрузить файл транзакций</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>Опции</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>Отчёты и сводка</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>Сохранить файл транзакций</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>Управление транзакциями</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>Вернуться в главное меню</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>Сохранять файл после каждого изменения (медленно)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>Сменить язык</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>Обзор счета</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>Обзор счета за период с</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>Обзор счета</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>Как вы хотите просмотреть отчёт?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>Категория с наибольшими расходами</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>Месячный отчёт</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>Нет транзакций в этом месяце. Отчёт невозможен.</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>Нет транзакций в этом году. Отчёт невозможен.</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>Постраничный просмотр (без очистки экрана)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>Постраничный просмотр</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>Экспортировать обзор счета и 12 месячных отчётов в Excel</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>Список</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>Общие расходы</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>Общий доход</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>Годовой отчёт</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>Загрузка тестовых транзакций...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>Тестовые транзакции загружены...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>По категории</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>По диапазону дат</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>Введите первую дату диапазона.</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>Введите вторую дату диапазона.</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>Первая дата</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>Как вы хотите искать транзакции?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>Транзакции не найдены. Попробуйте снова с другими параметрами.</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>Параметры применены</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>Сортировать по дате (возрастание)</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>Сортировать по дате (убывание)</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>Чередование цветов строк таблицы</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>По диапазону суммы</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>Поиск отменён</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>Нажмите любую клавишу для продолжения</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>Нажмите любую клавишу для выхода</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>Невозможно просмотреть без соответствующих транзакций.</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>Вы уверены, что хотите выйти? (Y) — выйти, любая другая клавиша — продолжить</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>Нажмите, чтобы выйти</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>Введите "exit" для отмены</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>Нажмите любую клавишу для подтверждения</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>Введите дату транзакции в следующем формате</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape или Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>Введите сумму дохода как положительное число..</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>Выберите месяц для просмотра отчёта</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>Введите описание транзакции</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>Выберите год для просмотра отчёта</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>Пред.: ↑ ← PgUp | След.: ↓ → PgDn | Выход: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Space или Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>Чтобы выйти/отменить</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>Чтобы загрузить</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>Чтобы пропустить</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>Добавить расходную транзакцию</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>Добавить доходную транзакцию</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>Добавление расхода для</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>Категория транзакции</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>Описание не указано</message>
            		</item>
            		<item key="TransMgnt_SearchTransactions">
            			<message>транзакции</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>Транзакция успешно добавлена!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>Просмотреть все транзакции</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>Переданный файл имеет неожидаемый формат!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>Сумма должна быть больше нуля и не может быть пустой.</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>Сумма должна быть больше или равна нулю и не может быть пустой.</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>Пожалуйста, используйте правильный формат даты</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>Неверный ввод! Попробуйте снова!</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>Культура не найдена для</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>Удаление транзакций удалит текущие данные и перезапишет файл при сохранении. Это действие необратимо. Сделайте резервную копию файла транзакций при необходимости.</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>Каталоги не найдены!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>Поле не может быть пустым или содержать только пробелы!</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>Нет доступа к файлу!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>Файл не найден!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>Исключение: null!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>Ошибка при загрузке файла!</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>Месяц должен быть числом от 1 до 12</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>Убедитесь, что дата не раньше допустимого предела</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>Дата не может быть в будущем.</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>Этот номер отсутствует в списке!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>Файл транзакций или бюджета не найден</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>XML имеет неправильный формат!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>Сохранение на диск...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>Нет транзакций для сохранения. Сохранение пропущено...</message>
            		</item>
            		<item key="Category_Income">
            			<message>Доход</message>
            		</item>

            		<item key="Category_Housing">
            			<message>Жильё</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>Продукты</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>Транспорт</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>Коммунальные услуги</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>Рестораны</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>Страхование</message>
            		</item>

            		<item key="Category_Debt">
            			<message>Долги</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>Развлечения</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>Здравоохранение</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>Переводы</message>
            		</item>

            		<item key="Category_Fees">
            			<message>Комиссии</message>
            		</item>

            		<item key="Category_Other">
            			<message>Другое</message>
            		</item>
            	</ru>

            	<hi>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>राशि स्वीकार की गई</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>राशि सीमा से अधिक नहीं हो सकती</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>राशि मान्य नहीं थी</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>बजट पार हो गया</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>शेष बजट देखें</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>वर्तमान शेष राशि देखें</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>बजट मेनू</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>बजट अपडेट नहीं हुआ!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>ऊपर दिए गए मेनू की कुंजी दबाएँ, नई राशि दर्ज करें और अपडेट के लिए Enter दबाएँ</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>मासिक बजट सेट करें</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>बजट श्रेणी अपडेट करें</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>बजट अपडेट हो गया!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>यदि कोई बॉक्स खाली है तो आप संपादन मोड में हैं</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>चेतावनी! आपने अपने बजट का 80% से अधिक उपयोग कर लिया है</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>यदि बजट 80% या 100% से अधिक हो जाए तो चेतावनी दिखाई जाएगी</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>भाषा चयन</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>भाषा लागू की गई!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>डिफ़ॉल्ट अंग्रेज़ी शब्दकोश पर वापस लौट रहे हैं</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>सभी लेन-देन हटाएँ</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>डेटा चयन</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>संग्रहीत लेन-देन की संख्या</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>डिस्क से लोड करें</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>नमूना डेटा लोड करें</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>लेन-देन के बिना शुरू करें</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>लेन-देन की संख्या प्रिंट करें</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>सभी लेन-देन हटा दिए गए</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>चेतावनी: नमूना डेटा का उपयोग आपकी फ़ाइल को अधिलेखित कर सकता है</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>यह केवल मेमोरी में मौजूद लेन-देन की गणना करता है</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>आपकी आय के आधार पर हम आपको उच्च-ब्याज प्रबंधित RRSP की पेशकश कर सकते हैं (7% वार्षिक रिटर्न)</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>आपकी आय के आधार पर हम आपको मध्यम-ब्याज प्रबंधित RRSP की पेशकश कर सकते हैं (4% वार्षिक)</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>आपकी आय के आधार पर आपातकालीन निधि के लिए बचत खाता खोलने की सलाह दी जाती है</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>आपकी आय के आधार पर 22% APR वाला क्रेडिट कार्ड उपलब्ध है</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>आपकी आय के आधार पर 12% APR वाला क्रेडिट कार्ड उपलब्ध है</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>हम मुफ्त क्रेडिट परामर्श और स्थिर आय पर ध्यान देने की सलाह देते हैं</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>आपकी वित्तीय स्थिति गंभीर प्रतीत होती है। कृपया तुरंत हमारे कार्यालय से संपर्क करें</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>बैंक सुझाव</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>इस महीने के लिए कोई लेन-देन नहीं है। यह दस्तावेज़ आपके रिकॉर्ड के लिए है</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>फ़ाइल तक पहुँच नहीं हो सकी। सुनिश्चित करें कि यह किसी अन्य प्रोग्राम में खुली नहीं है</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Excel फ़ाइल प्रोग्राम के उसी फ़ोल्डर में सहेजी गई</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>हमारे प्रोग्राम का उपयोग करने के लिए धन्यवाद। नीचे आपके खाते का एक संक्षिप्त सारांश है। अधिक विवरण अन्य वर्कशीट में उपलब्ध हैं।</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>वर्कशीट नहीं मिली!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>एक लेन-देन श्रेणी चुनें</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>चयन करने के लिए संबंधित कुंजी दबाएँ</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>ये दोनों तिथियाँ समान हैं! कृपया अलग तिथि दर्ज करें</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>तिथियों के बीच खोज की जा रही है</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>अपनी फ़ाइलों के लिए पासवर्ड की पुष्टि करें</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>अपनी फ़ाइलों के लिए पासवर्ड दर्ज करें</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>सुरक्षित फ़ाइल(ों) के लिए पासवर्ड दर्ज करें</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>कम से कम 15 अक्षर लंबा,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>एक अंक शामिल हो</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>बड़े और छोटे अक्षरों का मिश्रण हो</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>कम से कम एक विशेष वर्ण शामिल हो</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>ध्यान रखें, यह पासवर्ड कंप्यूटर पर संग्रहीत नहीं किया जाएगा। यदि आप इसे भूल जाते हैं तो अपने लेन-देन डेटा तक पहुँच खो देंगे!</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>एक सुरक्षित पासवर्ड होना चाहिए</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>एक लेन-देन की राशि एक ट्रिलियन से अधिक नहीं हो सकती, कृपया इसे छोटे लेन-देन में विभाजित करें</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>पासवर्ड मेल नहीं खाते!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>पासवर्ड आवश्यकताओं को पूरा नहीं करता</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>रद्द किया गया</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>व्यय लेन-देन जोड़ें</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>आय लेन-देन जोड़ें</message>
            		</item>
            		<item key="Label_All">
            			<message>सभी</message>
            		</item>
            		<item key="Label_Amount">
            			<message>राशि</message>
            		</item>
            		<item key="Label_And">
            			<message>और</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>की लागत पर</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>प्रयास</message>
            		</item>
            		<item key="Label_Balance">
            			<message>शेष राशि</message>
            		</item>
            		<item key="Label_Category">
            			<message>श्रेणी</message>
            		</item>
            		<item key="Label_Date">
            			<message>तिथि</message>
            		</item>
            		<item key="Label_Description">
            			<message>विवरण</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>व्यय</message>
            		</item>
            		<item key="Label_FileName">
            			<message>फ़ाइल नाम</message>
            		</item>
            		<item key="Label_Found">
            			<message>मिला</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>सबसे अधिक व्यय श्रेणी</message>
            		</item>
            		<item key="Label_Income">
            			<message>आय</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>निर्देश</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>मासिक</message>
            		</item>
            		<item key="Label_No">
            			<message>नहीं</message>
            		</item>
            		<item key="Label_Options">
            			<message>विकल्प</message>
            		</item>
            		<item key="Label_Or">
            			<message>या</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>पृष्ठ</message>
            		</item>
            		<item key="Label_Press">
            			<message>दबाएँ</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>खोज रद्द की गई</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>सेकंड</message>
            		</item>
            		<item key="Label_Starting">
            			<message>प्रारंभ</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>सारांश रद्द किया गया</message>
            		</item>
            		<item key="Label_To">
            			<message>से</message>
            		</item>
            		<item key="Label_Total">
            			<message>कुल</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>कुल शेष राशि</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>कुल व्यय</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>कुल आय</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>फिर से प्रयास करने के लिए</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>लेन-देन रद्द किया गया</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>वार्षिक</message>
            		</item>
            		<item key="Label_Years">
            			<message>वर्ष</message>
            		</item>
            		<item key="Label_Yes">
            			<message>हाँ</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>क्या आप केवल मिली हुई बजट फ़ाइल लोड करना चाहेंगे?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>क्या आप दूसरा पासवर्ड आज़माना चाहते हैं?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>अगले प्रयास से पहले प्रतीक्षा समय लागू होगा</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>पासवर्ड दर्ज करें</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>अन्य विकल्पों के लिए (जैसे नमूना डेटा)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>गलत पासवर्ड के बाद प्रतीक्षा समय</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>प्रोग्राम के उसी फ़ोल्डर में फ़ाइल नहीं मिली!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>पासवर्ड फ़ाइल से मेल नहीं खाता!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>लेन-देन के बिना शुरू करें</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>बहुत अधिक गलत पासवर्ड प्रयास</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>बहुत अधिक गलत पासवर्ड प्रयास</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>बजट उपकरण</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>मुख्य मेनू</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>लेन-देन फ़ाइल लोड करें</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>विकल्प</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>रिपोर्ट और सारांश</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>लेन-देन फ़ाइल सहेजें</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>लेन-देन प्रबंधन</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>मुख्य मेनू पर वापस जाएँ</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>हर परिवर्तन के बाद फ़ाइल सहेजें (धीमा)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>भाषा बदलें</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>खाता अवलोकन</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>से खाता सारांश</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>खाता सारांश</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>आप रिपोर्ट कैसे देखना चाहेंगे?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>सबसे अधिक व्यय श्रेणी</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>मासिक सारांश</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>इस महीने कोई लेन-देन नहीं है, सारांश नहीं दिखाया जा सकता</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>इस वर्ष कोई लेन-देन नहीं है, सारांश नहीं दिखाया जा सकता</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>पृष्ठ दृश्य (स्क्रीन साफ़ न करें)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>पृष्ठ दृश्य</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>खाता सारांश और 12 मासिक रिपोर्ट Excel में निर्यात करें</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>सूची दृश्य</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>कुल व्यय</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>कुल आय</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>वार्षिक सारांश</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>नमूना लेन-देन डेटा लोड किया जा रहा है...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>नमूना लेन-देन लोड हो गए...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>श्रेणी के अनुसार</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>तिथि सीमा के अनुसार</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>कृपया सीमा की पहली तिथि दर्ज करें</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>कृपया सीमा की दूसरी तिथि दर्ज करें</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>पहली तिथि है</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>आप लेन-देन कैसे खोजना चाहेंगे?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>कोई लेन-देन नहीं मिला, कृपया अलग मानों के साथ फिर प्रयास करें</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>विकल्प लागू किए गए</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>तिथि के अनुसार आरोही क्रम</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>तिथि के अनुसार अवरोही क्रम</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>बेहतर पढ़ने के लिए तालिका रंग बैंडिंग</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>मूल्य सीमा के अनुसार</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>खोज रद्द की गई</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>जारी रखने के लिए कोई भी कुंजी दबाएँ</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>बाहर निकलने के लिए कोई भी कुंजी दबाएँ</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>उपयुक्त लेन-देन के बिना आप इसे नहीं देख सकते।</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>क्या आप वाकई प्रोग्राम बंद करना चाहते हैं? बाहर निकलने के लिए (Y) दबाएँ, जारी रखने के लिए कोई भी अन्य कुंजी दबाएँ</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>बाहर निकलने के लिए दबाएँ</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>रद्द करने के लिए exit टाइप करें</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>स्वीकार करने के लिए कोई भी कुंजी दबाएँ</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>कृपया लेन-देन की तिथि निम्न प्रारूप में दर्ज करें:</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape या Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>कृपया आय की राशि सकारात्मक संख्या के रूप में दर्ज करें</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>जिस महीने का सारांश देखना चाहते हैं उसे चुनें</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>कृपया लेन-देन का विवरण दर्ज करें</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>जिस वर्ष का सारांश देखना चाहते हैं उसे चुनें</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>पिछला: ↑ ← PgUp | अगला: ↓ → PgDn | बाहर: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Space या Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>बाहर निकलने/रद्द करने के लिए</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>लोड करने के लिए</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>छोड़ने के लिए</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>व्यय लेन-देन जोड़ें</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>आय लेन-देन जोड़ें</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>के लिए व्यय जोड़ रहे हैं</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>लेन-देन श्रेणी</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>कोई विवरण प्रदान नहीं किया गया</message>
            		</item>
            		<item key="TransMgnt_SearchTransactions">
            			<message>लेन-देन</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>लेन-देन सफलतापूर्वक जोड़ा गया!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>सभी लेन-देन देखें</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>दी गई फ़ाइल का प्रारूप अपेक्षित नहीं है!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>राशि शून्य से अधिक होनी चाहिए और खाली नहीं हो सकती।</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>राशि शून्य से अधिक या उसके बराबर होनी चाहिए और खाली नहीं हो सकती।</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>कृपया सही तिथि प्रारूप का उपयोग करें</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>गलत इनपुट! फिर से प्रयास करें!</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>संस्कृति नहीं मिली</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>लेन-देन हटाने से वर्तमान डेटा हट जाएगा और सहेजने पर फ़ाइल अधिलेखित हो जाएगी। यह परिवर्तन स्थायी है। आवश्यकता हो तो बैकअप लें।</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>डायरेक्टरी नहीं मिली!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>खाली या केवल स्पेस नहीं हो सकता!</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>फ़ाइल तक पहुँच की अनुमति नहीं है!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>फ़ाइल नहीं मिली!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>Null अपवाद!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>फ़ाइल लोड करते समय त्रुटि हुई!</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>कृपया सुनिश्चित करें कि महीना 1 से 12 के बीच संख्या हो</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>सुनिश्चित करें कि तिथि निम्न सीमा से पहले की नहीं है</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>तारीख भविष्य में नहीं हो सकती।</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>यह संख्या सूची में नहीं है!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>कोई लेन-देन या बजट फ़ाइल नहीं मिली</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>XML सही प्रारूप में नहीं है!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>डिस्क पर सहेजा जा रहा है...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>सहेजने के लिए कोई लेन-देन नहीं, सहेजना छोड़ा जा रहा है...</message>
            		</item>
            		<item key="Category_Income">
            			<message>आय</message>
            		</item>

            		<item key="Category_Housing">
            			<message>आवास</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>किराना</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>परिवहन</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>उपयोगिता बिल</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>रेस्तरां</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>बीमा</message>
            		</item>

            		<item key="Category_Debt">
            			<message>ऋण</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>मनोरंजन</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>स्वास्थ्य सेवा</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>स्थानांतरण</message>
            		</item>

            		<item key="Category_Fees">
            			<message>शुल्क</message>
            		</item>

            		<item key="Category_Other">
            			<message>अन्य</message>
            		</item>
            	</hi>

            	<de>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>Betrag akzeptiert</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>Der Betrag ist zu hoch</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>Der Betrag ist ungültig</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>Budget überschritten</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>Aktuellen Kontostand prüfen</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>Aktuellen Kontostand prüfen</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>Budget-Menü</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>Budget nicht aktualisiert!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>Drücken Sie eine Menü-Taste oben, geben Sie den neuen Betrag ein und drücken Sie Enter, um zu aktualisieren.</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>Monatliches Budget festlegen</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>Budgetkategorie aktualisieren</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>Budget aktualisiert!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>Wenn ein Feld leer ist, befinden Sie sich im Bearbeitungsmodus.</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>Warnung! Sie haben mehr als 80 % Ihres Budgets erreicht</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>Warnungen werden angezeigt, wenn das Budget über 80 % oder 100 % liegt</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>Sprachauswahl</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>Sprache angewendet!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>Zurück zum englischen Standardwörterbuch</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>Alle Transaktionen löschen</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>Datenauswahl</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>Gespeicherte Transaktionen</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>Von Datenträger laden</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>Beispieldaten laden</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>Ohne Transaktionen starten</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>Anzahl der Transaktionen anzeigen</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>Alle Transaktionen gelöscht</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>Warnung: Das Laden von Beispieldaten und Änderungen können Ihre Transaktionsdatei überschreiben. Nur zu Testzwecken verwenden.</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>Es werden nur die aktuell im Speicher befindlichen Transaktionen gezählt.</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>Basierend auf Ihrem Einkommen können wir Ihnen ein hochverzinsliches verwaltetes RRSP mit garantierten 7 % Jahresrendite anbieten.</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>Basierend auf Ihrem Einkommen können wir Ihnen ein mittelverzinsliches verwaltetes RRSP mit garantierten 4 % Jahresrendite anbieten.</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>Basierend auf Ihrem Einkommen empfehlen wir die Eröffnung eines Sparkontos für einen Notfallfonds.</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>Basierend auf Ihrem Einkommen können wir Ihnen eine Kreditkarte mit 22 % APR und einem zinsfreien ersten Monat anbieten.</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>Basierend auf Ihrem Einkommen können wir Ihnen eine Kreditkarte mit 12 % APR und drei zinsfreien Monaten anbieten.</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>Basierend auf Ihrem Einkommen empfehlen wir kostenlose Schuldnerberatung und die Stabilisierung Ihres Einkommens.</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>Ihre finanzielle Situation scheint kritisch zu sein. Bitte besuchen Sie umgehend unsere Filiale, damit wir Lösungen besprechen können.</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>Bankempfehlungen</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>Für diesen Monat gibt es derzeit keine Transaktionen. Dieses Dokument dient zu Ihren Unterlagen.</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>Die Datei konnte nicht geöffnet werden. Stellen Sie sicher, dass sie nicht in einem anderen Programm geöffnet ist und Schreibrechte vorhanden sind.</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Excel-Datei im selben Ordner wie das Programm gespeichert.</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>Vielen Dank für die Nutzung unseres Programms. Nachfolgend finden Sie eine kurze Übersicht Ihres Kontos. Weitere Details befinden sich in den anderen Arbeitsblättern mit Informationen der letzten 12 Monate.</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>Arbeitsblatt nicht gefunden!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>Wählen Sie eine Transaktionskategorie</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>Drücken Sie die entsprechende Taste, um eine Auswahl zu treffen.</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>Diese Daten sind identisch! Bitte geben Sie ein anderes Datum ein.</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>Suche nach Daten zwischen</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>Bestätigen Sie das Passwort für Ihre Dateien</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>Geben Sie das Passwort für Ihre Dateien ein</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>Passwort für die gesicherten Dateien eingeben</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>Mindestens 15 Zeichen lang,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>Enthält eine Ziffer</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>Enthält Groß- und Kleinbuchstaben</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>Enthält mindestens ein Sonderzeichen</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>Dieses Passwort wird nicht gespeichert. Wenn Sie es vergessen, verlieren Sie den Zugriff auf Ihre Transaktionsdaten!</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>Ein sicheres Passwort ist</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>Eine einzelne Transaktion darf nicht mehr als eine Billion betragen. Bitte teilen Sie sie auf.</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>Die Passwörter stimmen nicht überein!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>Das Passwort erfüllt nicht die Anforderungen.</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>Abgebrochen</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>Ausgabentransaktion hinzufügen</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>Einnahmetransaktion hinzufügen</message>
            		</item>
            		<item key="Label_All">
            			<message>Alle</message>
            		</item>
            		<item key="Label_Amount">
            			<message>Betrag</message>
            		</item>
            		<item key="Label_And">
            			<message>und</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>zum Preis von</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>Versuch</message>
            		</item>
            		<item key="Label_Balance">
            			<message>Kontostand</message>
            		</item>
            		<item key="Label_Category">
            			<message>Kategorie</message>
            		</item>
            		<item key="Label_Date">
            			<message>Datum</message>
            		</item>
            		<item key="Label_Description">
            			<message>Beschreibung</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>Ausgaben</message>
            		</item>
            		<item key="Label_FileName">
            			<message>Dateiname</message>
            		</item>
            		<item key="Label_Found">
            			<message>Gefunden</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>Kategorie mit den höchsten Ausgaben</message>
            		</item>
            		<item key="Label_Income">
            			<message>Einnahmen</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>Anweisungen</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>Monatlich</message>
            		</item>
            		<item key="Label_No">
            			<message>Nein</message>
            		</item>
            		<item key="Label_Options">
            			<message>Optionen</message>
            		</item>
            		<item key="Label_Or">
            			<message>oder</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>S.</message>
            		</item>
            		<item key="Label_Press">
            			<message>Drücken Sie</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>Suche abgebrochen</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>s</message>
            		</item>
            		<item key="Label_Starting">
            			<message>Starten</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>Zusammenfassung abgebrochen</message>
            		</item>
            		<item key="Label_To">
            			<message>bis</message>
            		</item>
            		<item key="Label_Total">
            			<message>Gesamt</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>Gesamtsaldo</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>Gesamtausgaben</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>Gesamteinnahmen</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>Um es erneut zu versuchen</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>Transaktion abgebrochen</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>Jährlich</message>
            		</item>
            		<item key="Label_Years">
            			<message>Jahre</message>
            		</item>
            		<item key="Label_Yes">
            			<message>Ja</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>Möchten Sie die gefundene Budgetdatei trotzdem laden?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>Möchten Sie ein anderes Passwort versuchen?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>Beim nächsten Versuch wird eine Wartezeit angewendet.</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>Passwort eingeben</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>für andere Optionen (z. B. Beispieldaten)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>Wartezeit nach falschem Passwort</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>Datei im selben Verzeichnis wie das Programm nicht gefunden!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>Das Passwort stimmt nicht mit der Datei überein!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>Ohne Transaktionen starten</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>Zu viele falsche Passwortversuche.</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>Zu viele falsche Passwortversuche</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>Budgetwerkzeuge</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>Hauptmenü</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>Transaktionsdatei laden</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>Optionen</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>Berichte und Zusammenfassung</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>Transaktionsdatei speichern</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>Transaktionsverwaltung</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>Zum Hauptmenü zurückkehren</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>Datei nach jeder Änderung speichern (langsam)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>Sprache ändern</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>Kontenübersicht</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>Kontenübersicht von</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>Kontenübersicht</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>Wie möchten Sie den Bericht anzeigen?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>Kategorie mit den höchsten Ausgaben</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>Monatsübersicht</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>Keine Transaktionen in diesem Monat. Zusammenfassung kann nicht angezeigt werden.</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>Keine Transaktionen in diesem Jahr. Zusammenfassung kann nicht angezeigt werden.</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>Seitenansicht (Bildschirm nicht löschen)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>Seitenansicht</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>Kontenübersicht und 12 Monatsberichte in eine Excel-Datei exportieren</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>Listenansicht</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>Gesamtausgaben</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>Gesamteinnahmen</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>Jahresübersicht</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>Beispiel-Transaktionsdaten werden geladen...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>Beispieltransaktionen geladen...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>Nach Kategorie</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>Nach Datumsbereich</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>Geben Sie das erste Datum des Bereichs ein.</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>Geben Sie das zweite Datum des Bereichs ein.</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>Das erste Datum ist</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>Wie möchten Sie nach Transaktionen suchen?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>Keine Transaktionen gefunden. Versuchen Sie es mit anderen Suchparametern erneut.</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>Optionen angewendet</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>Nach Datum aufsteigend sortieren</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>Nach Datum absteigend sortieren</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>Tabellenfarbband für bessere Lesbarkeit</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>Nach Preisbereich</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>Suche abgebrochen</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>Beliebige Taste drücken, um fortzufahren</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>Beliebige Taste drücken, um zu beenden</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>Ohne passende Transaktionen kann nichts angezeigt werden.</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>Sind Sie sicher, dass Sie das Programm beenden möchten? (Y) zum Beenden, jede andere Taste zum Fortfahren</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>Drücken zum Beenden</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>Geben Sie "exit" ein, um abzubrechen</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>Beliebige Taste drücken, um zu bestätigen</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>Geben Sie das Transaktionsdatum im folgenden Format ein:</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape oder Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>Geben Sie einen positiven Einkommensbetrag ein</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>Wählen Sie den Monat für die Zusammenfassung</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>Geben Sie eine Beschreibung der Transaktion ein</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>Wählen Sie das Jahr für die Zusammenfassung</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>Zurück: ↑ ← PgUp | Weiter: ↓ → PgDn | Beenden: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Leertaste oder Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>Zum Beenden/Abbrechen</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>Zum Laden</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>Zum Überspringen</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>Ausgabentransaktion hinzufügen</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>Einnahmetransaktion hinzufügen</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>Ausgabe hinzufügen für</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>Transaktionskategorie</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>Keine Beschreibung angegeben</message>
            		</item>
            		<item key="TransMgnt_SearchTransactions">
            			<message>Nach einer Transaktionen suchen</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>Transaktion erfolgreich hinzugefügt!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>Alle Transaktionen anzeigen</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>Die übergebene Datei hat ein unerwartetes Format!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>Der Betrag muss größer als Null sein und darf nicht leer sein.</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>Der Betrag muss größer oder gleich null sein und darf nicht leer sein.</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>Bitte verwenden Sie das richtige Datumsformat.</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>Ungültige Eingabe! Bitte versuchen Sie es erneut!</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>Kultur nicht gefunden für</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>Das Löschen von Transaktionen entfernt aktuelle Daten und überschreibt beim Speichern die Datei. Diese Änderung ist dauerhaft. Sichern Sie Ihre Transaktionsdatei vorsichtshalber.</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>Verzeichnisse nicht gefunden!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>Darf nicht leer sein oder nur aus Leerzeichen bestehen!</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>Keine Berechtigung zum Zugriff auf die Datei!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>Datei nicht gefunden!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>Null-Ausnahme!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>Beim Laden der Datei ist ein Fehler aufgetreten!</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>Der Monat muss eine Zahl von 1 bis 12 sein</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>Stellen Sie sicher, dass das Datum nicht vor dem folgenden Grenzwert liegt</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>Das Datum darf nicht in der Zukunft liegen.</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>Diese Nummer steht nicht in der Liste!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>Keine Transaktionen oder Budgetdatei gefunden</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>XML ist nicht korrekt formatiert!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>Speichern auf Datenträger...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>Keine Transaktionen zum Speichern vorhanden, Speichern übersprungen...</message>
            		</item>
            		<item key="Category_Income">
            			<message>Einkommen</message>
            		</item>

            		<item key="Category_Housing">
            			<message>Wohnen</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>Lebensmittel</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>Transport</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>Nebenkosten</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>Restaurants</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>Versicherung</message>
            		</item>

            		<item key="Category_Debt">
            			<message>Schulden</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>Unterhaltung</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>Gesundheitswesen</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>Überweisungen</message>
            		</item>

            		<item key="Category_Fees">
            			<message>Gebühren</message>
            		</item>

            		<item key="Category_Other">
            			<message>Sonstiges</message>
            		</item>
            	</de>
            	<fr>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>Montant accepté</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>Le montant ne peut pas dépasser</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>Le montant n'était pas valide</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>Budget dépassé</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>Vérifier budget restant</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>Vérifier le solde restant</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>Menu budget</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>Budget non mis à jour!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>Appuyez sur une touche de menu ci-dessus, entrez le nouveau montant et appuyez sur entrée pour mettre à jour.</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>Définir budget mensuel</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>Mettre à jour catégorie budget</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>Budget mis à jour!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>Lorsqu'une case est vide vous êtes en mode édition.</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>Attention! Vous avez dépassé 80% de votre budget</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>Avertissements affichés si budget dépasse 80% ou 100%</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>Sélection de langue</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>Langue appliquée!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>Retour au dictionnaire anglais par défaut</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>Supprimer toutes les transactions</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>Sélection des données</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>Nombre de transactions stockées</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>Charger depuis disque</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>Charger données exemple</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>Démarrer sans transactions</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>Afficher le nombre de transactions</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>Toutes les transactions supprimées</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>Attention: Charger des données exemple et modifier une transaction ou un budget ÉCRASERA votre fichier de transactions. Utiliser les données exemple seulement pour les tests.</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>Ce nombre ne compte que ce qui est en mémoire, pas ce qui est dans le fichier.</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>Selon vos revenus, nous pouvons vous offrir un REER géré à haut rendement pour maximiser vos gains (7% garanti par an).</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>Selon vos revenus, nous pouvons vous offrir un REER géré à rendement moyen avec une garantie annuelle de 4%.</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>Selon vos revenus, nous suggérons d'ouvrir un compte d'épargne pour commencer à bâtir un fonds d'urgence.</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>Selon vos revenus, nous pouvons vous offrir une carte de crédit à intérêt élevé avec un TAEG de 22% et aucun intérêt le premier mois.</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>Selon vos revenus, nous pouvons vous offrir une carte de crédit à intérêt moyen avec un TAEG de 12% et trois mois sans intérêt.</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>Selon vos revenus, nous recommandons un service gratuit de conseil en crédit et de se concentrer sur l'obtention d'un revenu plus stable ou plus élevé.</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>Votre situation financière semble critique. Veuillez visiter notre bureau immédiatement afin que nous puissions discuter des solutions possibles.</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>Recommandations bancaires</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>Il n'y a actuellement aucune transaction pour ce mois. Ce document est fourni pour vos dossiers.</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>Le fichier n'a pas pu être accédé. Assurez-vous qu'il n'est pas ouvert dans un autre programme et que cette application a la permission d'écrire dans ce dossier.</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Feuille Excel sauvegardée dans le même dossier que l'exécutable.</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>Merci d'utiliser notre programme. Nous espérons que vous apprécierez ce bref résumé de votre compte. Des détails supplémentaires se trouvent dans d'autres feuilles contenant les informations des 12 derniers mois.</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>Feuille de calcul introuvable!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>Choisir une catégorie de transaction</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>Appuyez sur la touche correspondante pour faire un choix</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>Ces dates sont identiques! Vous devez entrer une date différente</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>Recherche des dates entre</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>Confirmer mot de passe pour vos fichiers</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>Entrez le mot de passe pour vos fichiers</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>Entrer mot de passe pour les fichiers sécurisés</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>Au moins 15 caractères,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>Contient un chiffre</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>Contient des lettres majuscules et minuscules</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>Contient au moins 1 caractère spécial</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>Rappelez-vous, ce mot de passe n'est pas stocké sur l'ordinateur. Vous devez le mémoriser sinon vous perdrez l'accès à votre base de données!</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>Un mot de passe sécurisé est</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>Une transaction ne peut pas dépasser un trillion, veuillez la diviser en transactions plus petites</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>Les mots de passe ne correspondent pas!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>Le mot de passe ne respecte pas les exigences.</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>Annulé</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>Ajouter une transaction de dépense</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>Ajouter une transaction de revenu</message>
            		</item>
            		<item key="Label_All">
            			<message>Tous</message>
            		</item>
            		<item key="Label_Amount">
            			<message>Montant</message>
            		</item>
            		<item key="Label_And">
            			<message>et</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>au coût de</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>Tentative</message>
            		</item>
            		<item key="Label_Balance">
            			<message>Solde</message>
            		</item>
            		<item key="Label_Category">
            			<message>Catégorie</message>
            		</item>
            		<item key="Label_Date">
            			<message>Date</message>
            		</item>
            		<item key="Label_Description">
            			<message>Description</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Entrée</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>Dépense</message>
            		</item>
            		<item key="Label_FileName">
            			<message>Nom du fichier</message>
            		</item>
            		<item key="Label_Found">
            			<message>Trouvé</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>Catégorie de dépense la plus élevée</message>
            		</item>
            		<item key="Label_Income">
            			<message>Revenu</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>Instructions</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>Mensuel</message>
            		</item>
            		<item key="Label_No">
            			<message>Non</message>
            		</item>
            		<item key="Label_Options">
            			<message>Options</message>
            		</item>
            		<item key="Label_Or">
            			<message>ou</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>pg.</message>
            		</item>
            		<item key="Label_Press">
            			<message>Appuyer</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>Recherche annulée</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>s</message>
            		</item>
            		<item key="Label_Starting">
            			<message>Démarrage</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>Résumé annulé</message>
            		</item>
            		<item key="Label_To">
            			<message>à</message>
            		</item>
            		<item key="Label_Total">
            			<message>Total</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>Solde total</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>Dépenses totales</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>Revenu total</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>Pour réessayer</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>Transaction annulée</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>Annuel</message>
            		</item>
            		<item key="Label_Years">
            			<message>Années</message>
            		</item>
            		<item key="Label_Yes">
            			<message>Oui</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>Voulez-vous charger seulement le fichier budget trouvé?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>Voulez-vous essayer un autre mot de passe?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>Refroidissement appliqué pour la prochaine tentative!</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>Entrez le mot de passe</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>pour autres options (comme données exemple)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>Refroidissement après mot de passe incorrect</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>Fichier introuvable dans le même dossier que l'exécutable!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>Le mot de passe ne correspond pas à celui attendu pour ce fichier!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>Démarrer sans transactions</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>Trop de tentatives de mot de passe incorrect.</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>Trop de tentatives de mot de passe incorrect</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>Outils de budget</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>Menu principal</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>Charger fichier de transactions</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>Options</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>Rapports et résumé</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>Sauvegarder fichier de transactions</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>Gestion des transactions</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>Retour au menu principal</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>Sauvegarder fichier après chaque modification (lent)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>Changer la langue</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>Aperçu du compte</message>
            		</item>
            		<item key="ReportAndSum_AccountSummary">
            			<message>Résumé du compte</message>
            		</item>
            		<item key="ReportAndSum_AcountSummaryFrom">
            			<message>Résumé du compte depuis</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>Comment voulez-vous afficher le rapport?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>Catégorie de dépense la plus élevée</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>Résumé mensuel</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>Aucune transaction dans ce mois, impossible d'afficher le résumé.</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>Aucune transaction dans cette année, impossible d'afficher le résumé.</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>Vue pages (ne pas effacer écran)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>Vue pages</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>Exporter le résumé du compte et 12 résumés mensuels dans un document Excel</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>Vue liste</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>Dépense totale</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>Revenu total</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>Résumé annuel</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>Chargement des transactions exemple...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>transactions exemple chargées...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>Par catégorie</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>Par plage de dates</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>Veuillez entrer la première date dans la plage.</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>Veuillez entrer la deuxième date dans la plage.</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>La première date est</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>Comment voulez-vous rechercher les transactions?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>Aucune transaction trouvée, essayez une recherche avec d'autres paramètres.</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>Options appliquées</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>Trier par date ascendante</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>Trier par date descendante</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>Bandes de couleur pour lecture plus facile</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>Par plage de prix</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>Recherche annulée</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>N'importe quelle touche pour continuer</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>N'importe quelle touche pour quitter</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>Vous ne pouvez pas afficher sans transactions appropriées.</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>Êtes-vous sûr de vouloir quitter? (Y) pour sortir, toute autre touche pour continuer</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>Appuyez pour quitter</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>Tapez exit pour abandonner</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>N'importe quelle touche pour confirmer</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>Veuillez entrer la date de la transaction dans le format suivant:</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape ou Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>Veuillez entrer un montant de revenu comme nombre positif</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>Sélectionnez le mois pour lequel vous voulez un résumé</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>Veuillez entrer une description de la transaction</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>Sélectionnez l'année pour laquelle vous voulez un résumé</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>Préc: ↑ ← PgUp | Suiv: ↓ → PgDn | Quitter: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Espace ou Entrée</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>Pour quitter/abandonner</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>Pour charger</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>Pour passer</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>Ajouter transaction de dépense</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>Ajouter transaction de revenu</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>Ajout d'une dépense pour</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>Catégorie de transaction</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>Aucune description fournie</message>
            		</item>
            		<item key="TransMgnt_SearchTransactions">
            			<message>Rechercher une transactions</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>Félicitations, transaction ajoutée!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>Voir toutes les transactions</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>Format de fichier inattendu!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>Le montant doit être supérieur à zéro et ne peut pas être vide.</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>Le montant doit être supérieur ou égal à zéro et ne peut pas être vide.</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>Veuillez utiliser la convention de date appropriée</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>Entrée invalide! Essayez encore!</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>Culture introuvable pour</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>Supprimer les transactions supprimera les transactions actuelles et modifier/sauvegarder écrasera le fichier. Une fois modifié ceci est permanent. Sauvegardez votre fichier de transactions si nécessaire.</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>Répertoires introuvables!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>Ne peut pas être vide ou seulement des espaces!</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>Non autorisé à accéder au fichier!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>Fichier introuvable!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>Exception nulle!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>Une erreur s'est produite lors du chargement du fichier!</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>Veuillez vous assurer que le mois est indiqué par un nombre de 1 à 12</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>Assurez-vous que la date n'est pas antérieure à la limite suivante</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>La date ne peut pas être dans le futur.</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>Ce numéro n'est pas dans la liste!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>Aucune transaction ou fichier budget trouvé</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>Xml mal formaté!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>Sauvegarde sur disque...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>Aucune transaction à sauvegarder, sauvegarde ignorée...</message>
            		</item>
            		<item key="Category_Income">
            			<message>Revenu</message>
            		</item>

            		<item key="Category_Housing">
            			<message>Logement</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>Épicerie</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>Transport</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>Services publics</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>Restaurants</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>Assurance</message>
            		</item>

            		<item key="Category_Debt">
            			<message>Dette</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>Divertissement</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>Soins de santé</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>Transferts</message>
            		</item>

            		<item key="Category_Fees">
            			<message>Frais</message>
            		</item>

            		<item key="Category_Other">
            			<message>Autre</message>
            		</item>
            	</fr>

            	<es>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>Monto aceptado</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>El monto no puede exceder</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>El monto no era válido</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>Presupuesto superado</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>Verificar presupuesto restante</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>Verificar saldo actual</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>Menú de presupuesto</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>¡Presupuesto no actualizado!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>Presione una tecla del menú anterior, escriba el nuevo monto y presione Enter para actualizar.</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>Establecer presupuesto mensual</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>Actualizar categoría de presupuesto</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>¡Presupuesto actualizado!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>Cuando una casilla está vacía, está en modo de edición.</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>¡Advertencia! Ha utilizado más del 80% de su presupuesto</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>Se mostrarán advertencias si el presupuesto supera el 80% o el 100%</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>Selección de idioma</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>¡Idioma aplicado!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>Volviendo al diccionario predeterminado en inglés</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>Eliminar todas las transacciones</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>Selección de datos</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>Cantidad de transacciones almacenadas</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>Cargar desde disco</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>Cargar datos de ejemplo</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>Iniciar sin transacciones</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>Mostrar cantidad de transacciones</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>Todas las transacciones eliminadas</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>Advertencia: cargar datos de ejemplo y agregar una transacción o actualizar una categoría de presupuesto\nSOBRESCRIBIRÁ su archivo de transacciones. Utilice datos de ejemplo solo para pruebas.</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>Este número solo cuenta lo que está en memoria, no lo que está en el archivo o se ha escrito en él.</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>Según sus ingresos, podemos ofrecerle un RRSP administrado de alto interés diseñado para maximizar sus ganancias (7% anual garantizado).</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>Según sus ingresos, podemos ofrecerle un RRSP administrado de interés medio diseñado para maximizar sus ganancias, con una garantía anual del 4%.</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>Según sus ingresos, le sugerimos abrir una cuenta de ahorros para comenzar a crear un fondo de emergencia.</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>Según sus ingresos, podemos ofrecerle una tarjeta de crédito de alto interés con un APR del 22% y sin intereses durante el primer mes.</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>Según sus ingresos, podemos ofrecerle una tarjeta de crédito de interés medio con un APR del 12% y tres meses sin intereses.</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>Según sus ingresos, recomendamos asesoramiento crediticio gratuito y centrarse en obtener ingresos más estables o mayores.</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>Su situación financiera parece crítica. Por favor visite nuestra oficina lo antes posible para discutir soluciones y ayudarle.</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>Recomendaciones bancarias</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>Actualmente no hay transacciones para este mes. Este documento se proporciona para sus registros.</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>No se pudo acceder al archivo. Asegúrese de que no esté abierto en otro programa y de que esta aplicación tenga permiso para escribir en la carpeta seleccionada.</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Hoja de cálculo de Excel guardada en la misma carpeta que el ejecutable.</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>Gracias por utilizar nuestro programa. Esperamos que disfrute este breve resumen de su cuenta. Puede encontrar más detalles en otras hojas de cálculo que incluyen información de los 12 meses más recientes.</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>¡Hoja de cálculo no encontrada!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>Seleccione una categoría de transacción</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>Presione la tecla correspondiente para elegir.</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>¡Estas son las mismas fechas! Debe introducir una fecha diferente.</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>Buscando en la lista fechas entre</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>Confirme la contraseña para sus archivos</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>Introduzca la contraseña para sus archivos</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>Introduzca la contraseña para los archivos seguros</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>Al menos 15 caracteres,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>Contiene un número</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>Incluye letras mayúsculas y minúsculas (al menos una de cada)</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>Contiene al menos un carácter especial</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>Recuerde: esta contraseña no se guarda en el ordenador. Debe recordarla o perderá acceso a su base de datos de transacciones.</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>Una contraseña segura es</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>Una sola transacción no puede superar un billón. Divídala en varias transacciones más pequeñas.</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>¡Las contraseñas no coinciden!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>La contraseña no cumple los requisitos.</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>Cancelado</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>Agregar transacción de gasto</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>Agregar transacción de ingreso</message>
            		</item>
            		<item key="Label_All">
            			<message>Todos</message>
            		</item>
            		<item key="Label_Amount">
            			<message>Monto</message>
            		</item>
            		<item key="Label_And">
            			<message>y</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>con un costo de</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>Intento</message>
            		</item>
            		<item key="Label_Balance">
            			<message>Saldo</message>
            		</item>
            		<item key="Label_Category">
            			<message>Categoría</message>
            		</item>
            		<item key="Label_Date">
            			<message>Fecha</message>
            		</item>
            		<item key="Label_Description">
            			<message>Descripción</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>salir</message>
            		</item>
            		<item key="Label_Expense">
            			<message>Gasto</message>
            		</item>
            		<item key="Label_FileName">
            			<message>Nombre del archivo</message>
            		</item>
            		<item key="Label_Found">
            			<message>Encontrados</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>Categoría de mayor gasto</message>
            		</item>
            		<item key="Label_Income">
            			<message>Ingreso</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>Instrucciones</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>Mensual</message>
            		</item>
            		<item key="Label_No">
            			<message>No</message>
            		</item>
            		<item key="Label_Options">
            			<message>Opciones</message>
            		</item>
            		<item key="Label_Or">
            			<message>o</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>pág.</message>
            		</item>
            		<item key="Label_Press">
            			<message>Presione</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>Búsqueda cancelada</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>s</message>
            		</item>
            		<item key="Label_Starting">
            			<message>Iniciando</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>Resumen cancelado</message>
            		</item>
            		<item key="Label_To">
            			<message>a</message>
            		</item>
            		<item key="Label_Total">
            			<message>Total</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>Saldo total</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>Gastos totales</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>Ingresos totales</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>Intentar nuevamente</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>Transacción cancelada</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>Anual</message>
            		</item>
            		<item key="Label_Years">
            			<message>Años</message>
            		</item>
            		<item key="Label_Yes">
            			<message>Sí</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>¿Desea cargar únicamente el archivo de presupuesto encontrado?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>¿Desea intentar otra contraseña?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>¡Se aplicará un tiempo de espera antes del próximo intento!</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>Ingrese la contraseña</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>para otras opciones (como datos de ejemplo)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>Tiempo de espera por contraseña incorrecta</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>¡Archivo no encontrado en el mismo directorio que el ejecutable!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>¡La contraseña no coincide con la esperada para el archivo!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>Iniciar sin transacciones</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>Demasiados intentos de contraseña incorrecta.</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>Demasiados intentos de contraseña incorrecta</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>Herramientas de presupuesto</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>Menú principal</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>Cargar archivo de transacciones</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>Opciones</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>Informes y resumen</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>Guardar archivo de transacciones</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>Gestión de transacciones</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>Volver al menú principal</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>Guardar archivo después de cada cambio (lento)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>Cambiar idioma</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>Resumen de cuenta</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>Resumen de cuenta desde</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>Resumen de cuenta</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>¿Cómo desea ver el informe?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>Categoría de mayor gasto</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>Resumen mensual</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>No hay transacciones este mes, no se puede mostrar el resumen.</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>No hay transacciones este año, no se puede mostrar el resumen.</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>Vista por páginas (sin limpiar pantalla)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>Vista por páginas</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>Exportar resumen de cuenta y 12 resúmenes mensuales a un documento de Excel</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>Vista de lista</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>Gastos totales</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>Ingresos totales</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>Resumen anual</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>Cargando datos de transacciones de ejemplo...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>Transacciones de ejemplo cargadas...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>Por categoría</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>Por rango de fechas</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>Introduzca la primera fecha del rango.</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>Introduzca la segunda fecha del rango.</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>La primera fecha es</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>¿Cómo desea buscar las transacciones?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>No se encontraron transacciones, intente nuevamente con diferentes parámetros.</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>Opciones aplicadas</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>Ordenar por fecha ascendente</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>Ordenar por fecha descendente</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>Bandas de color en la tabla para facilitar la lectura</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>Por rango de importes</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>Búsqueda cancelada</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>Presione cualquier tecla para continuar</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>Presione cualquier tecla para salir</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>No puede ver información sin transacciones disponibles.</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>¿Está seguro de que desea salir? (Y) para salir, cualquier otra tecla para continuar</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>Presione para salir</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>Escriba exit para cancelar la operación</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>Presione cualquier tecla para confirmar</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>Introduzca la fecha de la transacción en el siguiente formato:</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape o Retroceso</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>Introduzca un monto de ingreso como número positivo</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>Seleccione el mes para el que desea un resumen</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>Introduzca una descripción de la transacción</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>Seleccione el año para el que desea un resumen</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>Ant: ↑ ← PgUp | Sig: ↓ → PgDn | Salir: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Espacio o Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>Para salir/cancelar</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>Para cargar</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>Para omitir</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>Agregar transacción de gasto</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>Agregar transacción de ingreso</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>Agregando gasto para</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>Categoría de la transacción</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>No se proporcionó descripción</message>
            		</item>

            		<item key="TransMgnt_SearchTransactions">
            			<message>Buscar una transacciones</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>¡Transacción agregada con éxito!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>Mostrar todas las transacciones</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>¡El archivo proporcionado no tiene el formato esperado!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>El monto debe ser mayor que cero y no puede estar vacío.</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>La cantidad debe ser mayor o igual a cero y no puede estar vacía.</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>Utilice el formato de fecha correcto</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>¡Entrada inválida! Inténtelo nuevamente.</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>Cultura no encontrada para</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>Eliminar transacciones borrará las actuales y al guardar se sobrescribirá el archivo. Una vez realizado el cambio será permanente. Haga una copia de seguridad si es necesario.</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>¡Directorios no encontrados!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>No puede estar vacío ni contener solo espacios.</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>¡No está autorizado para acceder al archivo!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>¡Archivo no encontrado!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>¡Excepción nula!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>Ocurrió un error al cargar el archivo.</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>Asegúrese de que el mes esté especificado con un número del 1 al 12</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>Asegúrese de que la fecha no sea anterior al límite permitido</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>La fecha no puede estar en el futuro.</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>¡Ese número no está en la lista!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>No se encontraron transacciones ni archivo de presupuesto.</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>¡El XML no tiene el formato correcto!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>Guardando en disco...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>No hay transacciones para guardar, omitiendo guardado...</message>
            		</item>
            		<item key="Category_Income">
            			<message>Ingresos</message>
            		</item>

            		<item key="Category_Housing">
            			<message>Vivienda</message>
            		</item>

            		<item key="Category_Groceries">
            			<message>Comestibles</message>
            		</item>

            		<item key="Category_Transportation">
            			<message>Transporte</message>
            		</item>

            		<item key="Category_Utilities">
            			<message>Servicios públicos</message>
            		</item>

            		<item key="Category_Restaurants">
            			<message>Restaurantes</message>
            		</item>

            		<item key="Category_Insurance">
            			<message>Seguro</message>
            		</item>

            		<item key="Category_Debt">
            			<message>Deuda</message>
            		</item>

            		<item key="Category_Entertainment">
            			<message>Entretenimiento</message>
            		</item>

            		<item key="Category_Healthcare">
            			<message>Atención médica</message>
            		</item>

            		<item key="Category_Transfers">
            			<message>Transferencias</message>
            		</item>

            		<item key="Category_Fees">
            			<message>Comisiones</message>
            		</item>

            		<item key="Category_Other">
            			<message>Otro</message>
            		</item>
            	</es>
            	<pl>
            		<!-- BudgetMenu -->
            		<item key="BudgetMenu_AmountAccepted">
            			<message>Kwota zaakceptowana</message>
            		</item>
            		<item key="BudgetMenu_AmountExceeded">
            			<message>Kwota nie może przekroczyć</message>
            		</item>
            		<item key="BudgetMenu_AmountInvalid">
            			<message>Kwota była nieprawidłowa</message>
            		</item>
            		<item key="BudgetMenu_BudgetExceeded">
            			<message>Budżet przekroczony</message>
            		</item>
            		<item key="BudgetMenu_CheckRemainBudget">
            			<message>Sprawdź pozostały budżet</message>
            		</item>
            		<item key="BudgetMenu_CurrentBalance">
            			<message>Sprawdź aktualne saldo</message>
            		</item>
            		<item key="BudgetMenu_Header">
            			<message>Menu budżetu</message>
            		</item>
            		<item key="BudgetMenu_NotUpdated">
            			<message>Budżet nie został zaktualizowany!</message>
            		</item>
            		<item key="BudgetMenu_SelectionInstruction">
            			<message>Naciśnij klawisz menu powyżej, wpisz nową kwotę i naciśnij Enter aby zaktualizować.</message>
            		</item>
            		<item key="BudgetMenu_SetMonthlyBudget">
            			<message>Ustaw miesięczny budżet</message>
            		</item>
            		<item key="BudgetMenu_UpdateBudgetCateg">
            			<message>Aktualizuj kategorię budżetu</message>
            		</item>
            		<item key="BudgetMenu_Updated">
            			<message>Budżet zaktualizowany!</message>
            		</item>
            		<item key="BudgetMenu_UpdateInstruction">
            			<message>Gdy pole jest puste, jesteś w trybie edycji.</message>
            		</item>
            		<item key="BudgetMenu_Warning80PercentOverBudget">
            			<message>Ostrzeżenie! Przekroczyłeś 80% swojego budżetu</message>
            		</item>
            		<item key="BudgetMenu_WarningInstruction">
            			<message>Ostrzeżenia pojawią się gdy budżet przekroczy 80% lub 100%</message>
            		</item>
            		<!-- ChooseLang -->
            		<item key="ChooseLang_Header">
            			<message>Wybór języka</message>
            		</item>
            		<item key="ChooseLang_LangApplied">
            			<message>Język zastosowany!</message>
            		</item>
            		<item key="ChooseLang_RevertingToEng">
            			<message>Powrót do domyślnego słownika angielskiego</message>
            		</item>
            		<!-- DataOptions -->
            		<item key="DataOptions_DeleteTransactions">
            			<message>Usuń wszystkie transakcje</message>
            		</item>
            		<item key="DataOptions_Header">
            			<message>Wybór danych</message>
            		</item>
            		<item key="DataOptions_LabelAmountOfTrans">
            			<message>Liczba zapisanych transakcji</message>
            		</item>
            		<item key="DataOptions_LoadFile">
            			<message>Załaduj z dysku</message>
            		</item>
            		<item key="DataOptions_LoadSample">
            			<message>Załaduj dane przykładowe</message>
            		</item>
            		<item key="DataOptions_NoloadOrSamples">
            			<message>Uruchom bez transakcji</message>
            		</item>
            		<item key="DataOptions_PrintTransactionCount">
            			<message>Wyświetl liczbę transakcji</message>
            		</item>
            		<item key="DataOptions_TransactionsDeleted">
            			<message>Wszystkie transakcje usunięte</message>
            		</item>
            		<item key="DataOptions_WarningSavingWithNoDataMayOverwrite">
            			<message>Ostrzeżenie: Załadowanie danych przykładowych i dodanie transakcji lub aktualizacja kategorii budżetu\nNADPISZE Twój plik transakcji. Używaj danych przykładowych tylko do testów.</message>
            		</item>
            		<item key="DataOptions_WarningThisPrintsOnlyRam">
            			<message>Ta liczba obejmuje tylko dane w pamięci, nie dane zapisane w pliku.</message>
            		</item>
            		<!-- Excel -->
            		<item key="Excel_BankRec1">
            			<message>Na podstawie Twojego dochodu możemy zaoferować zarządzany RRSP o wysokim oprocentowaniu zaprojektowany w celu maksymalizacji zysków (7% rocznie gwarantowane).</message>
            		</item>
            		<item key="Excel_BankRec2">
            			<message>Na podstawie Twojego dochodu możemy zaoferować zarządzany RRSP o średnim oprocentowaniu zaprojektowany w celu maksymalizacji zysków z gwarancją 4% rocznie.</message>
            		</item>
            		<item key="Excel_BankRec3">
            			<message>Na podstawie Twojego dochodu sugerujemy otwarcie konta oszczędnościowego w celu rozpoczęcia budowy funduszu awaryjnego.</message>
            		</item>
            		<item key="Excel_BankRec4">
            			<message>Na podstawie Twojego dochodu możemy zaoferować kartę kredytową o wysokim oprocentowaniu z RRSO 22% oraz brakiem odsetek przez pierwszy miesiąc.</message>
            		</item>
            		<item key="Excel_BankRec5">
            			<message>Na podstawie Twojego dochodu możemy zaoferować kartę kredytową o średnim oprocentowaniu z RRSO 12% oraz trzema miesiącami bez odsetek.</message>
            		</item>
            		<item key="Excel_BankRec6">
            			<message>Na podstawie Twojego dochodu zalecamy bezpłatne doradztwo kredytowe oraz skupienie się na uzyskaniu bardziej stabilnego lub wyższego dochodu.</message>
            		</item>
            		<item key="Excel_BankRec7">
            			<message>Twoja sytuacja finansowa wydaje się krytyczna. Prosimy o pilną wizytę w naszym oddziale w celu omówienia rozwiązań.</message>
            		</item>
            		<item key="Excel_BankRecommendations">
            			<message>Rekomendacje bankowe</message>
            		</item>
            		<item key="Excel_EmptyMonth">
            			<message>Obecnie brak transakcji w tym miesiącu. Dokument jest udostępniony do Twoich zapisów.</message>
            		</item>
            		<item key="Excel_FileNoAccessMessage">
            			<message>Nie można uzyskać dostępu do pliku. Upewnij się, że nie jest otwarty w innym programie oraz że aplikacja ma uprawnienia do zapisu w wybranym folderze.</message>
            		</item>
            		<item key="Excel_SavedMessage">
            			<message>Arkusz Excel zapisany w tym samym folderze co plik wykonywalny.</message>
            		</item>
            		<item key="Excel_WelcomeMessage">
            			<message>Dziękujemy za korzystanie z naszego programu. Mamy nadzieję, że spodoba Ci się to krótkie podsumowanie Twojego konta. Dodatkowe szczegóły można znaleźć w innych arkuszach obejmujących informacje z ostatnich 12 miesięcy.</message>
            		</item>
            		<item key="Excel_WorksheetNotFound">
            			<message>Nie znaleziono arkusza!</message>
            		</item>
            		<!-- GetCategory -->
            		<item key="GetCategory_ChooseCategory">
            			<message>Wybierz kategorię transakcji</message>
            		</item>
            		<item key="GetCategory_InstructionHowMakeChoice">
            			<message>Naciśnij odpowiedni klawisz, aby dokonać wyboru.</message>
            		</item>
            		<!-- GetDate -->
            		<item key="GetDate_SameDates">
            			<message>To są te same daty! Musisz wprowadzić inną datę</message>
            		</item>
            		<item key="GetDate_SearchingDatesBetween">
            			<message>Wyszukiwanie w liście dat pomiędzy</message>
            		</item>
            		<!-- GetPwd -->
            		<item key="GetPwd_ConfirmPw">
            			<message>Potwierdź hasło do swoich plików</message>
            		</item>
            		<item key="GetPwd_EnterPw">
            			<message>Wprowadź hasło do swoich plików</message>
            		</item>
            		<item key="GetPwd_Header">
            			<message>Wprowadź hasło do bezpiecznego pliku</message>
            		</item>
            		<item key="GetPwd_Instruction15Chars">
            			<message>Co najmniej 15 znaków,</message>
            		</item>
            		<item key="GetPwd_InstructionContainDigit">
            			<message>Zawiera cyfrę</message>
            		</item>
            		<item key="GetPwd_InstructionMixCase">
            			<message>Zawiera małe i wielkie litery (co najmniej jedna z każdej)</message>
            		</item>
            		<item key="GetPwd_InstructionSpecialChar">
            			<message>Zawiera co najmniej jeden znak specjalny</message>
            		</item>
            		<item key="GetPwd_PwSafteyReminder">
            			<message>Pamiętaj, że hasło nie jest przechowywane na komputerze. Musisz je zapamiętać, w przeciwnym razie utracisz dostęp do bazy transakcji!</message>
            		</item>
            		<item key="GetPwd_SecurePwIsHeader">
            			<message>Bezpieczne hasło to</message>
            		</item>
            		<item key="GetPwd_Warning_OverOneTrillionWarning">
            			<message>Pojedyncza transakcja nie może przekraczać jednego biliona, podziel ją na mniejsze transakcje.</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMatch">
            			<message>Hasła nie są zgodne!</message>
            		</item>
            		<item key="GetPwd_Warning_PwDontMeetCriteria">
            			<message>Hasło nie spełnia wymagań.</message>
            		</item>
            		<!-- Label -->
            		<item key="Label_Aborted">
            			<message>Przerwano</message>
            		</item>
            		<item key="Label_AddExpenseTransaction">
            			<message>Dodaj transakcję wydatku</message>
            		</item>
            		<item key="Label_AddIncomeTransaction">
            			<message>Dodaj transakcję przychodu</message>
            		</item>
            		<item key="Label_All">
            			<message>Wszystkie</message>
            		</item>
            		<item key="Label_Amount">
            			<message>Kwota</message>
            		</item>
            		<item key="Label_And">
            			<message>i</message>
            		</item>
            		<item key="Label_AtCostOf">
            			<message>o wartości</message>
            		</item>
            		<item key="Label_Attempt">
            			<message>Próba</message>
            		</item>
            		<item key="Label_Balance">
            			<message>Saldo</message>
            		</item>
            		<item key="Label_Category">
            			<message>Kategoria</message>
            		</item>
            		<item key="Label_Date">
            			<message>Data</message>
            		</item>
            		<item key="Label_Description">
            			<message>Opis</message>
            		</item>
            		<item key="Label_Enter">
            			<message>Enter</message>
            		</item>
            		<item key="Label_Exit">
            			<message>exit</message>
            		</item>
            		<item key="Label_Expense">
            			<message>Wydatek</message>
            		</item>
            		<item key="Label_FileName">
            			<message>Nazwa pliku</message>
            		</item>
            		<item key="Label_Found">
            			<message>Znaleziono</message>
            		</item>
            		<item key="Label_HighestExpenseCategory">
            			<message>Kategoria z najwyższymi wydatkami</message>
            		</item>
            		<item key="Label_Income">
            			<message>Przychód</message>
            		</item>
            		<item key="Label_Instructions">
            			<message>Instrukcje</message>
            		</item>
            		<item key="Label_Monthly">
            			<message>Miesięcznie</message>
            		</item>
            		<item key="Label_No">
            			<message>Nie</message>
            		</item>
            		<item key="Label_Options">
            			<message>Opcje</message>
            		</item>
            		<item key="Label_Or">
            			<message>lub</message>
            		</item>
            		<item key="Label_PageAbreviated">
            			<message>str.</message>
            		</item>
            		<item key="Label_Press">
            			<message>Naciśnij</message>
            		</item>
            		<item key="Label_SearchAborted">
            			<message>Wyszukiwanie przerwane</message>
            		</item>
            		<item key="Label_SForSecond">
            			<message>s</message>
            		</item>
            		<item key="Label_Starting">
            			<message>Uruchamianie</message>
            		</item>
            		<item key="Label_SummaryAborted">
            			<message>Podsumowanie przerwane</message>
            		</item>
            		<item key="Label_To">
            			<message>do</message>
            		</item>
            		<item key="Label_Total">
            			<message>Razem</message>
            		</item>
            		<item key="Label_TotalBalances">
            			<message>Łączne saldo</message>
            		</item>
            		<item key="Label_TotalExpenses">
            			<message>Łączne wydatki</message>
            		</item>
            		<item key="Label_TotalIncome">
            			<message>Łączny przychód</message>
            		</item>
            		<item key="Label_toTryAgain">
            			<message>Aby spróbować ponownie</message>
            		</item>
            		<item key="Label_TransactionAborted">
            			<message>Transakcja przerwana</message>
            		</item>
            		<item key="Label_Yearly">
            			<message>Rocznie</message>
            		</item>
            		<item key="Label_Years">
            			<message>Lata</message>
            		</item>
            		<item key="Label_Yes">
            			<message>Tak</message>
            		</item>
            		<!-- LoadFile -->
            		<item key="LoadFile_ConfirmLoadingBudgetFileOnly">
            			<message>Czy chcesz mimo to załadować znaleziony plik budżetu?</message>
            		</item>
            		<item key="LoadFile_ConfrimTryAnotherPw">
            			<message>Czy chcesz spróbować innego hasła?</message>
            		</item>
            		<item key="LoadFile_CooldownForNextAttempt">
            			<message>Czas oczekiwania zostanie zastosowany przed następną próbą!</message>
            		</item>
            		<item key="LoadFile_EnterPwForTransactionFile">
            			<message>Wprowadź hasło</message>
            		</item>
            		<item key="LoadFile_ForOtherOptionsSampleData">
            			<message>dla innych opcji (np. danych przykładowych)</message>
            		</item>
            		<item key="LoadFile_IncorrectPwCoolDown">
            			<message>Czas oczekiwania po błędnym haśle</message>
            		</item>
            		<item key="LoadFile_NoFileFound">
            			<message>Nie znaleziono pliku w tym samym katalogu co plik wykonywalny!</message>
            		</item>
            		<item key="LoadFile_PwIncorrect">
            			<message>Hasło nie odpowiada temu oczekiwanemu dla pliku!</message>
            		</item>
            		<item key="LoadFile_ToAbortStartNoTrans">
            			<message>Uruchom bez transakcji</message>
            		</item>
            		<item key="LoadFile_TooManyIncorrect">
            			<message>Zbyt wiele błędnych prób hasła.</message>
            		</item>
            		<item key="LoadFile_TooManyWrongPwAttempts">
            			<message>Zbyt wiele błędnych prób hasła</message>
            		</item>
            		<!-- MainMenu -->
            		<item key="MainMenu_BudgetTools">
            			<message>Narzędzia budżetu</message>
            		</item>
            		<item key="MainMenu_Header">
            			<message>Menu główne</message>
            		</item>
            		<item key="MainMenu_Load">
            			<message>Załaduj plik transakcji</message>
            		</item>
            		<item key="MainMenu_Options">
            			<message>Opcje</message>
            		</item>
            		<item key="MainMenu_ReportsAndSummary">
            			<message>Raporty i podsumowanie</message>
            		</item>
            		<item key="MainMenu_Save">
            			<message>Zapisz plik transakcji</message>
            		</item>
            		<item key="MainMenu_TransactionManagement">
            			<message>Zarządzanie transakcjami</message>
            		</item>
            		<!-- Menu -->
            		<item key="Menu_HeaderOuterDecor">
            			<message>------------</message>
            		</item>
            		<item key="Menu_Return">
            			<message>Powrót do głównego menu</message>
            		</item>
            		<!-- Options -->
            		<item key="Options_AutoSave">
            			<message>Zapisuj plik po każdej zmianie (wolniejsze)</message>
            		</item>
            		<item key="Options_ChangLang">
            			<message>Zmień język</message>
            		</item>
            		<!-- ReportAndSum -->
            		<item key="ReportAndSum_AccountOverview">
            			<message>Przegląd konta</message>
            		</item>
            		<item key="ReportAndSum_AccountSummaryFrom">
            			<message>Podsumowanie konta od</message>
            		</item>
            		<item key="ReportAndSum_AcountSummary">
            			<message>Podsumowanie konta</message>
            		</item>
            		<item key="ReportAndSum_AskHowToView">
            			<message>Jak chcesz wyświetlić raport?</message>
            		</item>
            		<item key="ReportAndSum_HighestExpenseCategory">
            			<message>Kategoria o najwyższych wydatkach</message>
            		</item>
            		<item key="ReportAndSum_MonthlySummary">
            			<message>Podsumowanie miesięczne</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInMonth">
            			<message>Brak transakcji w tym miesiącu, nie można wyświetlić podsumowania.</message>
            		</item>
            		<item key="ReportAndSum_NoTRansactionsInYear">
            			<message>Brak transakcji w tym roku, nie można wyświetlić podsumowania.</message>
            		</item>
            		<item key="ReportAndSum_PageAndScrollNoClear">
            			<message>Widok stron (bez czyszczenia ekranu)</message>
            		</item>
            		<item key="ReportAndSum_Pages">
            			<message>Widok stron</message>
            		</item>
            		<item key="ReportAndSum_SaveExcel">
            			<message>Eksportuj podsumowanie konta i 12 miesięcznych podsumowań do dokumentu Excel</message>
            		</item>
            		<item key="ReportAndSum_Scroll">
            			<message>Widok listy</message>
            		</item>
            		<item key="ReportAndSum_TotalExpense">
            			<message>Łączny wydatek</message>
            		</item>
            		<item key="ReportAndSum_TotalIncome">
            			<message>Łączny przychód</message>
            		</item>
            		<item key="ReportAndSum_YearlySummary">
            			<message>Podsumowanie roczne</message>
            		</item>
            		<!-- Sample -->
            		<item key="Sample_Header">
            			<message>Ładowanie przykładowych danych transakcji...</message>
            		</item>
            		<item key="Sample_Loaded">
            			<message>Załadowano przykładowe transakcje...</message>
            		</item>
            		<!-- SrcByTrans -->
            		<item key="SrcByTrans_Category">
            			<message>Według kategorii</message>
            		</item>
            		<item key="SrcByTrans_DateRange">
            			<message>Według zakresu dat</message>
            		</item>
            		<item key="SrcByTrans_EnterDate1">
            			<message>Wprowadź pierwszą datę w zakresie.</message>
            		</item>
            		<item key="SrcByTrans_EnterDate2">
            			<message>Wprowadź drugą datę w zakresie dat.</message>
            		</item>
            		<item key="SrcByTrans_FirstDateIs">
            			<message>Pierwsza data to</message>
            		</item>
            		<item key="SrcByTrans_HeaderQuestion">
            			<message>Jak chcesz wyszukiwać transakcje?</message>
            		</item>
            		<item key="SrcByTrans_NoResultSrcAgain">
            			<message>Nie znaleziono transakcji, spróbuj ponownie z innymi parametrami.</message>
            		</item>
            		<item key="SrcByTrans_OptionApplied">
            			<message>Zastosowane opcje</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderAsc">
            			<message>Sortuj według daty rosnąco</message>
            		</item>
            		<item key="SrcByTrans_OptionOrderDesc">
            			<message>Sortuj według daty malejąco</message>
            		</item>
            		<item key="SrcByTrans_OptionTableColorBanding">
            			<message>Kolorowe pasy tabeli dla łatwiejszego czytania</message>
            		</item>
            		<item key="SrcByTrans_PriceRange">
            			<message>Według zakresu kwot</message>
            		</item>
            		<item key="SrcByTrans_SrcAborted">
            			<message>Wyszukiwanie przerwane</message>
            		</item>
            		<!-- System -->
            		<item key="System_AnyKeyToContinue">
            			<message>Naciśnij dowolny klawisz, aby kontynuować</message>
            		</item>
            		<item key="System_AnyKeyToExit">
            			<message>Naciśnij dowolny klawisz, aby zakończyć</message>
            		</item>
            		<item key="System_NoReleventTransactions">
            			<message>Nie można wyświetlić bez odpowiednich transakcji.</message>
            		</item>
            		<item key="System_YToQuitProgram">
            			<message>Czy na pewno chcesz zakończyć? (Y) aby wyjść, dowolny inny klawisz aby kontynuować</message>
            		</item>
            		<!-- SystemInstructions -->
            		<item key="SystemInstructions_PressToExit">
            			<message>Naciśnij, aby wyjść</message>
            		</item>
            		<item key="SystemInstructions_Abort">
            			<message>Wpisz exit aby przerwać</message>
            		</item>
            		<item key="SystemInstructions_AnyKeyToAck">
            			<message>Dowolny klawisz aby potwierdzić</message>
            		</item>
            		<item key="SystemInstructions_EnterDate">
            			<message>Wprowadź datę transakcji w następującym formacie:</message>
            		</item>
            		<item key="SystemInstructions_EscapeOrBackspace">
            			<message>Escape lub Backspace</message>
            		</item>
            		<item key="SystemInstructions_InputIncomeAmount">
            			<message>Wprowadź kwotę przychodu jako liczbę dodatnią</message>
            		</item>
            		<item key="SystemInstructions_InputMonthForSummary">
            			<message>Wybierz miesiąc dla którego chcesz zobaczyć podsumowanie</message>
            		</item>
            		<item key="SystemInstructions_InputTransDescription">
            			<message>Wprowadź opis transakcji</message>
            		</item>
            		<item key="SystemInstructions_InputYearForSummary">
            			<message>Wybierz rok dla którego chcesz zobaczyć podsumowanie</message>
            		</item>
            		<item key="SystemInstructions_PageView">
            			<message>Poprz: ↑ ← PgUp | Nast: ↓ → PgDn | Wyjście: Esc Q ⌫</message>
            		</item>
            		<item key="SystemInstructions_SpaceOrEnter">
            			<message>Spacja lub Enter</message>
            		</item>
            		<item key="SystemInstructions_ToExitOrAbort">
            			<message>Aby wyjść/przerwać</message>
            		</item>
            		<item key="SystemInstructions_ToLoad">
            			<message>Aby załadować</message>
            		</item>
            		<item key="SystemInstructions_ToSkip">
            			<message>Aby pominąć</message>
            		</item>
            		<!-- TransMgnt -->
            		<item key="TransMgnt_AddExpenseTransaction">
            			<message>Dodaj transakcję wydatku</message>
            		</item>
            		<item key="TransMgnt_AddIncomeTransaction">
            			<message>Dodaj transakcję przychodu</message>
            		</item>
            		<item key="TransMgnt_AddingExpenseFor">
            			<message>Dodawanie wydatku dla</message>
            		</item>
            		<item key="TransMgnt_LabelTransCategory">
            			<message>Kategoria transakcji</message>
            		</item>
            		<item key="TransMgnt_NoDscProvided">
            			<message>Nie podano opisu</message>
            		</item>

            		<item key="TransMgnt_SearchTransactions">
            			<message>Wyszukaj transakcje</message>
            		</item>
            		<item key="TransMgnt_TransactionAdded">
            			<message>Gratulacje, transakcja została dodana!</message>
            		</item>
            		<item key="TransMgnt_ViewAllTransactions">
            			<message>Wyświetl wszystkie transakcje</message>
            		</item>
            		<!-- Warning -->
            		<item key="Warning_ArgumentIssue">
            			<message>Przekazany plik ma nieoczekiwany format!</message>
            		</item>
            		<item key="Warning_BadAmountNoZero">
            			<message>Kwota musi być większa od zera i nie może być pusta.</message>
            		</item>
            		<item key="Warning_BadAmountZeroOk">
            			<message>Kwota musi być większa lub równa zero i nie może być pusta.</message>
            		</item>
            		<item key="Warning_BadDate">
            			<message>Użyj poprawnego formatu daty</message>
            		</item>
            		<item key="Warning_BadInput">
            			<message>Nieprawidłowe dane! Spróbuj ponownie!</message>
            		</item>
            		<item key="Warning_CultureNotFound">
            			<message>Nie znaleziono kultury dla</message>
            		</item>
            		<item key="Warning_DateFormat">
            			<message>dd/MM/yyyy</message>
            		</item>
            		<item key="Warning_DateFormatYYYY">
            			<message>yyyy</message>
            		</item>
            		<item key="Warning_DeleteTransactions">
            			<message>Usunięcie transakcji usunie bieżące dane, a zapis nadpisze plik. Po dokonaniu zmiany będzie ona trwała. W razie potrzeby wykonaj kopię zapasową pliku transakcji.</message>
            		</item>
            		<item key="Warning_DirectoriesNotFound">
            			<message>Nie znaleziono katalogów!</message>
            		</item>
            		<item key="Warning_EmptyOrSpaces">
            			<message>Nie może być puste ani zawierać samych spacji!</message>
            		</item>
            		<item key="Warning_FileNotAuthorized">
            			<message>Brak uprawnień do dostępu do pliku!</message>
            		</item>
            		<item key="Warning_FileNotFound">
            			<message>Nie znaleziono pliku!</message>
            		</item>
            		<item key="Warning_FileNull">
            			<message>Błąd wartości null!</message>
            		</item>
            		<item key="Warning_GeneralException">
            			<message>Wystąpił błąd podczas ładowania pliku!</message>
            		</item>
            		<item key="Warning_InvalidMonth">
            			<message>Upewnij się, że miesiąc jest określony liczbą od 1 do 12</message>
            		</item>
            		<item key="Warning_InvalidYearOld">
            			<message>Upewnij się, że data nie jest wcześniejsza niż dopuszczalny limit</message>
            		</item>
            		<item key="Warning_InvalidYearNew">
            			<message>Data nie może być w przyszłości.</message>
            		</item>
            		<item key="Warning_LanguageNotInList">
            			<message>Ta liczba nie znajduje się na liście!</message>
            		</item>
            		<item key="Warning_NoTransactionsOrBudgetFound">
            			<message>Nie znaleziono transakcji ani pliku budżetu</message>
            		</item>
            		<item key="Warning_XmlFormat">
            			<message>Nieprawidłowy format XML!</message>
            		</item>
            		<!-- Write -->
            		<item key="Write_Saved">
            			<message>Zapisywanie na dysku...</message>
            		</item>
            		<item key="Write_SkipSaving">
            			<message>Brak transakcji do zapisania, pomijanie zapisu...</message>
            		</item>
            	<item key="Category_Income">
            		<message>Dochód</message>
            	</item>

            	<item key="Category_Housing">
            		<message>Mieszkanie</message>
            	</item>

            	<item key="Category_Groceries">
            		<message>Artykuły spożywcze</message>
            	</item>

            	<item key="Category_Transportation">
            		<message>Transport</message>
            	</item>

            	<item key="Category_Utilities">
            		<message>Media</message>
            	</item>

            	<item key="Category_Restaurants">
            		<message>Restauracje</message>
            	</item>

            	<item key="Category_Insurance">
            		<message>Ubezpieczenie</message>
            	</item>

            	<item key="Category_Debt">
            		<message>Dług</message>
            	</item>

            	<item key="Category_Entertainment">
            		<message>Rozrywka</message>
            	</item>

            	<item key="Category_Healthcare">
            		<message>Opieka zdrowotna</message>
            	</item>

            	<item key="Category_Transfers">
            		<message>Przelewy</message>
            	</item>

            	<item key="Category_Fees">
            		<message>Opłaty</message>
            	</item>

            	<item key="Category_Other">
            		<message>Inne</message>
            	</item>
            	</pl>
            	<en>
            	</en>
            </lang>



            """;

        //Creates an empty dictionary to populate the active language into
        public static Dictionary<MessageEnum, string> messageOutput = new();

        // ISO 639 language code to specify language to load from xml.
        static string language = "en";

        // Used to populate the dictionary if the xml file is missing.
        // This allows the program to still run and display messages even if there are issues with the language file.
        // It also serves as a reference for what messages need to be included in the xml file for a new language.
        static Dictionary<MessageEnum, string> defaultEnglishMessages = new() {
            { MessageEnum.System_AnyKeyToContinue, "Any key to continue" },
            { MessageEnum.System_AnyKeyToExit, "Any key to exit" },
            { MessageEnum.System_YToQuitProgram, "Are you sure you want to quit? (Y) to exit, any other key to continue" },
            { MessageEnum.System_NoReleventTransactions, "You can't view without appropriate transactions." },
            { MessageEnum.SystemInstructions_AnyKeyToAck, "Any key to acknowledge" },
            { MessageEnum.Warning_BadInput, "Bad Input! Try again!" },
            { MessageEnum.Warning_BadAmountNoZero, "Amount must be greater than zero and cannot be blank." },
            { MessageEnum.Warning_BadAmountZeroOk, "Amount must be greater or equal to zero and cannot be blank." },
            { MessageEnum.SystemInstructions_Abort, "Type exit to abort" },
            { MessageEnum.SystemInstructions_InputIncomeAmount, "Please enter an income amount as a positive number" },
            { MessageEnum.SystemInstructions_InputTransDescription, "Please enter a description of the transaction" },
            { MessageEnum.SystemInstructions_EnterDate, "Please enter the transaction date in the following format:" },
            { MessageEnum.SystemInstructions_PressToExit, "Press to exit" },
            { MessageEnum.Warning_BadDate, "Please use the proper date format" },
            { MessageEnum.Warning_DateFormat, "dd/MM/yyyy" },
            { MessageEnum.Warning_DateFormatYYYY, "yyyy" },
            { MessageEnum.Warning_EmptyOrSpaces, "Can't be empty or spaces only!" },
            { MessageEnum.Warning_CultureNotFound, "Culture not found for" },
            { MessageEnum.Warning_LanguageNotInList, "That Number is not in the list!" },
            { MessageEnum.Warning_XmlFormat, "Xml not formated properly!" },
            { MessageEnum.Warning_FileNotAuthorized, "Not authorized to access the file!" },
            { MessageEnum.Warning_ArgumentIssue, "File passed not expected format!" },
            { MessageEnum.Warning_FileNotFound, "File not found!" },
            { MessageEnum.Warning_DirectoriesNotFound, "Directories not found!" },
            { MessageEnum.Warning_FileNull, "Null exception!" },
            { MessageEnum.Warning_GeneralException, "An error occured while loading the file!" },
            { MessageEnum.Warning_NoTransactionsOrBudgetFound, "No transactions Or Budget File found" },
            { MessageEnum.SystemInstructions_EscapeOrBackspace, "Escape or Backspace" },
            { MessageEnum.SystemInstructions_ToExitOrAbort, "To exit/abort" },
            { MessageEnum.SystemInstructions_InputYearForSummary, "Select the year you would like a summary for" },
            { MessageEnum.SystemInstructions_InputMonthForSummary, "Select the month you would like a summary for" },
            { MessageEnum.SystemInstructions_SpaceOrEnter, "Space or Enter" },
            { MessageEnum.SystemInstructions_ToSkip, "To skip" },
            { MessageEnum.SystemInstructions_ToLoad, "To load" },
            { MessageEnum.Label_toTryAgain, "To try again" },
            { MessageEnum.SystemInstructions_PageView, "Prev: ↑ ← PgUp | Next: ↓ → PgDn | Exit: Esc Q ⌫" },
            { MessageEnum.Label_Exit, "exit" },//intentionally lowercase
            { MessageEnum.Label_Enter, "Enter" },
            { MessageEnum.Label_Or, "or" },
            { MessageEnum.Label_To, "to" },
            { MessageEnum.Label_Yes, "Yes" },
            { MessageEnum.Label_No, "No" },
            { MessageEnum.Label_Aborted, "Aborted" },
            { MessageEnum.Label_TransactionAborted, "Transaction aborted" },
            { MessageEnum.Label_SummaryAborted, "Summary aborted" },
            { MessageEnum.Label_SearchAborted, "Search aborted" },
            { MessageEnum.Label_FileName, "File Name" },
            { MessageEnum.Label_Starting, "Starting" },
            { MessageEnum.Label_Options, "Options" },
            { MessageEnum.Label_All, "All" },
            { MessageEnum.Label_And, "and" },
            { MessageEnum.Label_Yearly, "Yearly" },
            { MessageEnum.Label_Years, "Years" },
            { MessageEnum.Label_Monthly, "Monthly" },
            { MessageEnum.Label_Date, "Date" },
            { MessageEnum.Label_Amount, "Amount" },
            { MessageEnum.Label_Description, "Description" },
            { MessageEnum.Label_AddIncomeTransaction, "Add Income Transaction" },
            { MessageEnum.Label_AddExpenseTransaction, "Add Expense Transaction" },
            { MessageEnum.Label_Income, "Income" },
            { MessageEnum.Label_Total, "Total" },
            { MessageEnum.Label_Balance, "Balance" },
            { MessageEnum.Label_Expense, "Expense" },
            { MessageEnum.Label_Category, "Category" },
            { MessageEnum.Label_Press, "Press" },
            { MessageEnum.Label_Instructions, "Instructions" },
            { MessageEnum.Label_PageAbreviated, "pg." },
            { MessageEnum.Label_TotalIncome, "Total Income" },
            { MessageEnum.Label_TotalExpenses, "Total Expenses" },
            { MessageEnum.Label_TotalBalances, "Total Balance" },
            { MessageEnum.Label_HighestExpenseCategory, "Highest Expense Category" },
            { MessageEnum.Label_AtCostOf, "at a cost of" },
            { MessageEnum.Label_Attempt, "Attempt" },
            { MessageEnum.Label_SForSecond, "s" },
            { MessageEnum.Label_Found, "Found" },
            { MessageEnum.Menu_Return, "Return to main menu" },
            { MessageEnum.Menu_HeaderOuterDecor, "------------" },
            { MessageEnum.MainMenu_Header, "Main menu" },
            { MessageEnum.MainMenu_TransactionManagement, "Transaction management" },
            { MessageEnum.MainMenu_BudgetTools, "Budget tools" },
            { MessageEnum.MainMenu_ReportsAndSummary, "Reports And summary" },
            { MessageEnum.MainMenu_Load, "Load transaction file" },
            { MessageEnum.MainMenu_Save, "Save transaction file" },
            { MessageEnum.MainMenu_Options, "Options" },
            { MessageEnum.TransMgnt_AddIncomeTransaction, "Add income transaction" },
            { MessageEnum.TransMgnt_AddExpenseTransaction, "Add expense transaction" },
            { MessageEnum.TransMgnt_ViewAllTransactions, "View all transactions" },
            { MessageEnum.TransMgnt_SearchTransactions, "Search for transactions" },
            { MessageEnum.TransMgnt_TransactionAdded, "Congratulations, transaction added!" },
            { MessageEnum.TransMgnt_AddingExpenseFor, "Adding expense for" },
            { MessageEnum.TransMgnt_NoDscProvided, "No description provided" },
            { MessageEnum.TransMgnt_LabelTransCategory, "Transaction category" },
            { MessageEnum.SrcByTrans_HeaderQuestion, "How would you like to search transactions?" },
            { MessageEnum.SrcByTrans_DateRange, "By date range" },
            { MessageEnum.SrcByTrans_PriceRange, "By price range" },
            { MessageEnum.SrcByTrans_Category, "By category" },
            { MessageEnum.SrcByTrans_OptionOrderAsc, "Order by ascending date" },
            { MessageEnum.SrcByTrans_OptionOrderDesc, "Order by descending date" },
            { MessageEnum.SrcByTrans_OptionTableColorBanding, "Table color banding for easier reading" },
            { MessageEnum.SrcByTrans_OptionApplied, "Options applied" },
            { MessageEnum.SrcByTrans_NoResultSrcAgain, "No transactions found, try searching again with different parameters." },
            { MessageEnum.SrcByTrans_SrcAborted, "Search aborted" },
            { MessageEnum.SrcByTrans_EnterAmount1, "Please enter the first amount in the range." },
            { MessageEnum.SrcByTrans_EnterAmount2, "Please enter the second amount in the range." },
            { MessageEnum.SrcByTrans_FirstAmountIs, "The first amount is" },
            { MessageEnum.SrcByTrans_EnterDate1, "Please enter the first date in the range." },
            { MessageEnum.SrcByTrans_FirstDateIs, "The first date is" },
            { MessageEnum.SrcByTrans_EnterDate2, "Please enter the second date in the date range." },
            { MessageEnum.Warning_InvalidYearOld, "Date can't be earlier than the following limit" },
            { MessageEnum.Warning_InvalidYearNew, "Date cannot be in the future." },
            { MessageEnum.Warning_InvalidMonth, "Please ensure the month is specified by a number from 1 to 12" },
            { MessageEnum.Warning_DeleteTransactions, "Deleting transactions will delete current transactions and editing/saving will overwrite the file. Once you make a change this is permanent. Back up your transaction file if needed, just incase." },
            { MessageEnum.BudgetMenu_Header, "Budget Menu" },
            { MessageEnum.BudgetMenu_SetMonthlyBudget, "Set monthly budget" },
            { MessageEnum.BudgetMenu_UpdateBudgetCateg, "Update budget category" },
            { MessageEnum.BudgetMenu_CheckRemainBudget, "Check remaining budget" },
            { MessageEnum.BudgetMenu_Warning80PercentOverBudget, "Warning! You are more then 80Percent over your budget" },
            { MessageEnum.BudgetMenu_SelectionInstruction, "Press a menu key above, type in the new amount and press enter to update." },
            { MessageEnum.BudgetMenu_UpdateInstruction, "When a box is blank you are in edit mode." },
            { MessageEnum.BudgetMenu_WarningInstruction, "Warnings displayed if budget is over 80% or 100%" },
            { MessageEnum.BudgetMenu_AmountExceeded, "Amount can not exceed" },
            { MessageEnum.BudgetMenu_AmountAccepted, "Amount acccepted" },
            { MessageEnum.BudgetMenu_AmountInvalid, "The amount wasn't valid" },
            { MessageEnum.BudgetMenu_NotUpdated, "Budget not updated!" },
            { MessageEnum.BudgetMenu_Updated, "Budget updated!" },
            { MessageEnum.BudgetMenu_BudgetExceeded, "Budget exceeded" },
            { MessageEnum.BudgetMenu_CurrentBalance, "Check current balance" },
            { MessageEnum.ReportAndSum_AccountOverview, "Account overview" },
            { MessageEnum.ReportAndSum_YearlySummary, "Yearly summary" },
            { MessageEnum.ReportAndSum_MonthlySummary, "Monthly summary" },
            { MessageEnum.ReportAndSum_SaveExcel, "Export Account summary and 12 monthly summaries to excel document" },
            { MessageEnum.ReportAndSum_TotalIncome, "Total income" },
            { MessageEnum.ReportAndSum_TotalExpense, "Total expense" },
            { MessageEnum.ReportAndSum_HighestExpenseCategory, "Highest expense category" },
            { MessageEnum.ReportAndSum_AskHowToView, "How would you like to view the report?" },
            { MessageEnum.ReportAndSum_Scroll, "List view" },
            { MessageEnum.ReportAndSum_Pages, "Page view" },
            { MessageEnum.ReportAndSum_PageAndScrollNoClear, "Page View (Don't Clear Screen)" },
            { MessageEnum.ReportAndSum_NoTRansactionsInYear, "No transactions in this year, can't display summary." },
            { MessageEnum.ReportAndSum_NoTRansactionsInMonth, "No transactions in this month, can't display summary." },
            { MessageEnum.ReportAndSum_AccountSummaryFrom, "Account summary from" },
            { MessageEnum.ReportAndSum_AcountSummary, "Account summary" },
            { MessageEnum.Excel_WorksheetNotFound, "Worksheet not found!" },
            { MessageEnum.Excel_WelcomeMessage, "Thank you for using our program. We hope you enjoy this brief summary of your account. Additional details can be found in other worksheets, which includes information from the 12 most recent months." },
            { MessageEnum.Excel_BankRecommendations, "Bank recommendations" },
            { MessageEnum.Excel_BankRec1, "Based on your income, we can offer you a high-interest managed RRSP designed to help maximize your gains (7% year-over-year guaranteed)." },
            { MessageEnum.Excel_BankRec2, "Based on your income, we can offer you a medium-interest managed RRSP designed to help maximize your gains, with a 4% year-over-year guarantee." },
            { MessageEnum.Excel_BankRec3, "Based on your income, we suggest opening a savings account to begin building an emergency fund." },
            { MessageEnum.Excel_BankRec4, "Based on your income, we can offer you a high-interest credit card with a 22% APR and no interest for the first month." },
            { MessageEnum.Excel_BankRec5, "Based on your income, we can offer you a medium-interest credit card with a 12% APR and three months of no interest." },
            { MessageEnum.Excel_BankRec6, "Based on your income, we recommend free credit counseling and focusing on securing more stable or higher income." },
            { MessageEnum.Excel_BankRec7, "Your financial situation appears to be critical. Please visit our office immediately so we can discuss solutions and assist you further." },
            { MessageEnum.Excel_EmptyMonth, "Currently there are no transactions for this Month. This document is provided for your records." },
            { MessageEnum.Excel_SavedMessage, "Excel spreadsheet saved to same folder as executable." },
            { MessageEnum.Excel_FileNoAccessMessage, "The file could not be accessed. Please ensure it is not currently open in another program and that this application has permission to write to the selected folder." },
            { MessageEnum.DataOptions_Header, "Data Selection" },
            { MessageEnum.DataOptions_LoadFile, "Load from disk" },
            { MessageEnum.DataOptions_LoadSample, "Load sample data" },
            { MessageEnum.DataOptions_NoloadOrSamples, "Start without transactions" },
            { MessageEnum.DataOptions_WarningSavingWithNoDataMayOverwrite, "Warnning: Loading Sample Data and adding a transaction or updating a budget catagory\nWILL overwrite your transaction file. Use Sample Data for testing only." },
            { MessageEnum.DataOptions_DeleteTransactions, "Delete all transactions" },
            { MessageEnum.DataOptions_PrintTransactionCount, "Print amount of transactions" },
            { MessageEnum.DataOptions_LabelAmountOfTrans, "Amount of transactions stored" },
            { MessageEnum.DataOptions_WarningThisPrintsOnlyRam, "This amount only counts what is in memory, not what is in the file, or has been written" },
            { MessageEnum.DataOptions_TransactionsDeleted, "All transactions deleted" },
            { MessageEnum.Options_ChangLang, "Change language" },
            { MessageEnum.Options_AutoSave, "Save file after every change (slow)" },
            { MessageEnum.GetPwd_Header, "Enter password for the secure file(s)" },
            { MessageEnum.GetPwd_PwSafteyReminder, "Remember, this password is not stored on the computer. You must remember your password or you will loose access to your transaction database!" },
            { MessageEnum.GetPwd_SecurePwIsHeader, "A secure password is" },
            { MessageEnum.GetPwd_Instruction15Chars, "Atleast 15 characters long," },
            { MessageEnum.GetPwd_InstructionContainDigit, "Contains a digit" },
            { MessageEnum.GetPwd_InstructionSpecialChar, "Contains atleast 1 special character" },
            { MessageEnum.GetPwd_InstructionMixCase, "Consists of mixed case letters (atleast one upper and one lower case)" },
            { MessageEnum.GetPwd_EnterPw, "Enter the password for your files" },
            { MessageEnum.GetPwd_ConfirmPw, "Confirm the password for your files" },
            { MessageEnum.GetPwd_Warning_PwDontMatch, "The passwords did not match!" },
            { MessageEnum.GetPwd_Warning_PwDontMeetCriteria, "The password does not meet the requirments." },
            { MessageEnum.GetPwd_Warning_OverOneTrillionWarning, "A single transaction can't have an amount over one trillion, please break up into smaller transactions" },
            { MessageEnum.GetCategory_ChooseCategory, "Choose a Transaction Category" },
            { MessageEnum.GetCategory_InstructionHowMakeChoice, "Press the corresponding key to make a choice." },
            { MessageEnum.GetDate_SameDates, "These are the same dates! You will have to enter a different date" },
            { MessageEnum.GetDate_SearchingDatesBetween, "Searching list for dates between" },
            { MessageEnum.ChooseLang_Header, "Language Selection" },
            { MessageEnum.ChooseLang_RevertingToEng, "Reverting to default English Dictionary" },
            { MessageEnum.ChooseLang_LangApplied, "Langauge applied!" },
            { MessageEnum.LoadFile_TooManyIncorrect, "Too many incorrect password attempts." },
            { MessageEnum.LoadFile_EnterPwForTransactionFile, "Enter password" },
            { MessageEnum.LoadFile_IncorrectPwCoolDown, "Incorrect password cool down" },
            { MessageEnum.LoadFile_ToAbortStartNoTrans, "Start without transactions" },
            { MessageEnum.LoadFile_NoFileFound, "File not found in the same directory as exacutable!" },
            { MessageEnum.LoadFile_ConfirmLoadingBudgetFileOnly, "Would you still like to load the found budget file?" },
            { MessageEnum.LoadFile_PwIncorrect, "The password does not match what is expected for the file!" },
            { MessageEnum.LoadFile_ForOtherOptionsSampleData, "for other options (such as sample data)" },
            { MessageEnum.LoadFile_ConfrimTryAnotherPw, "Do you want try another password?" },
            { MessageEnum.LoadFile_TooManyWrongPwAttempts, "Too many wrong password attempts" },
            { MessageEnum.LoadFile_CooldownForNextAttempt, "Cool down will be in effect for the next attempt!" },
            { MessageEnum.Sample_Header, "Loading sample transaction data..." },
            { MessageEnum.Sample_Loaded, "sample transactions loaded..." },
            { MessageEnum.Write_SkipSaving, "No transactions to save, skipping save..." },
            { MessageEnum.Write_Saved, "Saving data..." },
            { MessageEnum.Category_Income, "Income" },
            { MessageEnum.Category_Housing, "Housing" },
            { MessageEnum.Category_Groceries, "Groceries" },
            { MessageEnum.Category_Transportation, "Transportation" },
            { MessageEnum.Category_Utilities, "Utilities" },
            { MessageEnum.Category_Restaurants, "Restaurants" },
            { MessageEnum.Category_Insurance, "Insurance" },
            { MessageEnum.Category_Debt, "Debt" },
            { MessageEnum.Category_Entertainment, "Entertainment" },
            { MessageEnum.Category_Healthcare, "Healthcare" },
            { MessageEnum.Category_Transfers, "Transfers" },
            { MessageEnum.Category_Fees, "Fees" },
            { MessageEnum.Category_Other, "Other" }

        };

        static Dictionary<TransactionCategory, MessageEnum> TransactionCategoryToLanguage = new() {
            { TransactionCategory.Income,  MessageEnum.Category_Income },
            { TransactionCategory.Housing,  MessageEnum.Category_Housing },
            { TransactionCategory.Groceries,  MessageEnum.Category_Groceries },
            { TransactionCategory.Transportation,  MessageEnum.Category_Transportation },
            { TransactionCategory.Utilities,  MessageEnum.Category_Utilities },
            { TransactionCategory.Restaurants,  MessageEnum.Category_Restaurants },
            { TransactionCategory.Insurance,  MessageEnum.Category_Insurance },
            { TransactionCategory.Debt,  MessageEnum.Category_Debt },
            { TransactionCategory.Entertainment,  MessageEnum.Category_Entertainment },
            { TransactionCategory.Healthcare,  MessageEnum.Category_Healthcare },
            { TransactionCategory.Transfers,  MessageEnum.Category_Transfers },
            { TransactionCategory.Fees,  MessageEnum.Category_Fees },
            { TransactionCategory.Other,  MessageEnum.Category_Other },

        };
        #endregion

        #region >>> // Console Color Control Related
        /// <summary>
        /// Dictionary for grouped colors. ColorGroup enum As Key and value is an 2 length array, Foreground ConsoleColor, and Background ConsoleColor.
        /// </summary>
        public static Dictionary<ColorGroup, ConsoleColor [ ]> colorByGroup = new() {
            {ColorGroup.Default, [ConsoleColor.White, ConsoleColor.Black ] },
            {ColorGroup.SystemWarning, [ConsoleColor.Red, ConsoleColor.Black ] },

            {ColorGroup.SystemError, [ConsoleColor.Red, ConsoleColor.White ] },
            {ColorGroup.SystemInstructions, [ConsoleColor.Cyan, ConsoleColor.Black ] },
            {ColorGroup.SystemInstructionsGray, [ConsoleColor.Gray, ConsoleColor.Black ] },
            {ColorGroup.MenuHeadings, [ConsoleColor.Cyan, ConsoleColor.Black ] },
            {ColorGroup.MenuItems, [ConsoleColor.Green, ConsoleColor.Black ] },
            {ColorGroup.Success, [ConsoleColor.Black, ConsoleColor.Yellow ] },
            {ColorGroup.Header, [ConsoleColor.Yellow, ConsoleColor.Black ] },
            {ColorGroup.InputStyleA, [ConsoleColor.Black, ConsoleColor.White ] },
            {ColorGroup.InputStyleText, [ConsoleColor.Yellow, ConsoleColor.Black ] },
            {ColorGroup.SystemPromptHint, [ConsoleColor.Gray, ConsoleColor.Black ] },
            {ColorGroup.SystemPromptInstructions, [ConsoleColor.Cyan, ConsoleColor.Black ] }
        };
        //commonly reused colors
        static ConsoleColor [ ] MenuHeadings = colorByGroup [ ColorGroup.MenuHeadings ];
        static ConsoleColor [ ] MenuItemColor = colorByGroup [ ColorGroup.MenuItems ];
        #endregion

        #region >>> // Date formating related
        /// <summary>
        /// Stores format string + expected max display length. For multi languages it may be good to allow mixed variation or just store in outputMessages
        /// </summary>
        static private Dictionary<DateFormatEnum, Tuple<string, int>> dateFormatDictionary = new(){
            { DateFormatEnum.NumberMonth, new Tuple<string, int>("dd/MM/yyyy", 10) },
            { DateFormatEnum.ShortMonth,  new Tuple<string, int>("dd/MMM/yyyy", 11) },
            { DateFormatEnum.LONGMonth,   new Tuple<string, int>("dd/MMMM/yyyy", 17) }};
        // Input format (for parsing user entry)
        static Tuple<string, int> dateFormatInput = dateFormatDictionary [ DateFormatEnum.NumberMonth ];
        // Output format (for displaying on screen)
        static Tuple<string, int> dateFormatOut = dateFormatDictionary [ DateFormatEnum.ShortMonth ];
        #endregion

        #region >>> // Enums for menu state control (reusing same method for multiple jobs)
        /// <summary>
        /// Specify which type of transaction is being inputed (logic very similar)
        /// </summary>
        enum IncomeOrExpense {
            Income,
            Expense
        }
        enum BudgetOrTransaction {
            Budget,
            Transaction
        }
        #endregion

        //Longest string length of catagories, used for formatting the screen. This is assigned by method that checks enum
        //name lengths and uses the largest one. (prevents issue if enum is updated by user or programer (future implementation)

        static int transactionCategoryLongestSize;

        // Setup Excel
        static IXLWorksheet? ws;
        static XLWorkbook? workbook;

        //File names for loading and saving transactions and budget.
        static string transactionFileName = @"transactiondata.dat";
        static string budgetFileName = @"budgetdata.dat";

        //Password for loading sample data, and accessing options menu. This is to prevent users from accidentally loading sample data and overwriting their transactions, or changing settings without knowing it.
        static string? password;
        static bool PasswordCorrect = false;
        static int attemptNumber = 1;

        //Configuration object to hold settings that can be updated in the options menu and used throughout the program.
        static Configuration config = new();
        static string configFile = "settings.config";


        //Indicates whether the application saves state after every transaction.
        static bool saveOnEveryTransaction = true;

        //Master list for all transactions
        static List<Transaction> Transactions = new();

        //Holds each budget catagories amount
        static Dictionary<TransactionCategory, decimal> BudgetCategories = new();

        // Fatrthest back user can specify for year 
        static int cutofdate = 120;

        #region >>> /// Main menu and related methods
        /// <summary>
        /// Initializes the console application and presents the main menu for user interaction.
        /// </summary>
        /// <remarks>This method manages the application's main loop, allowing users to access transaction
        /// management, budgeting, reporting, and options menus. It also handles language selection and file loading
        /// based on user preferences.</remarks>
        /// <param name="args">No args used</param>
        static void Main( string [ ] args ) {
            Console.CursorVisible = false;
            Console.ForegroundColor = colorByGroup [ ColorGroup.Default ] [ 0 ]; //Console Color Default Set
            Console.BackgroundColor = colorByGroup [ ColorGroup.Default ] [ 1 ]; //incase color/information from previous program carried over
            //Used to aid translation dictionary. Copy dictionary of defualtEnglish to Ai, as for it to redefine in new language.
            //and define that in the xport file//ExportLanguageFile(); ExportLanguageFile();
            introAnimation();
            Console.Write( "\x1b[3J" ); Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8; //Enable displaying other languages
            Console.InputEncoding = System.Text.Encoding.UTF8;
            // Allows the user to choose his/her language or skip if alrad defined in a loaded config file.
            ChooseLanguage( colorByGroup [ ColorGroup.SystemError ] );
            //itterage through the Transaction Categories and check the name lengsths for formatting.
            transactionCategoryLongestSize = GetTransactionCategoryLongestLength(); //must be after ChooseLangauge
            //Load file logic and menu
            LoadFileMenu();
            //Main Menu Logic and Input Validation
            while ( true ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                //Display menu choices
                MainMenuChoice();
                switch ( Console.ReadKey( intercept: true ).Key ) {
                    //Transaction Menu
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        TransactionManagementMenu();
                        break;
                    //Budget Menu
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        BudgetMenu();
                        break;
                    //Reports & Summary
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ReportAndSummaryMenu();
                        break;
                    //Options
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        OptionsMenu();
                        break;
                    //Save transactions when option is set to not save on every Transaction and Budget update. (Good for reducing lag, bad for data loss)
                    case ConsoleKey.D5 when !saveOnEveryTransaction:
                    case ConsoleKey.NumPad5 when !saveOnEveryTransaction:
                        LoadFile();
                        break;
                    //Load transactions when option is set to not save on every Transaction and Budget update. (Assumes file only needs to be loaded on startup)
                    case ConsoleKey.D6 when !saveOnEveryTransaction:
                    case ConsoleKey.NumPad6 when !saveOnEveryTransaction:
                        WriteTransactionsAndBudget( BudgetOrTransaction.Transaction );
                        break;
                    //Exit program with confirmation
                    case ConsoleKey.Escape:
                    case ConsoleKey.Backspace:
                        Console.WriteLine();
                        Console.WriteLine( messageOutput [ MessageEnum.System_YToQuitProgram ] );
                        if ( Console.ReadKey( intercept: true ).Key == ConsoleKey.Y ) {
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.Label_Exit ], colorByGroup [ ColorGroup.SystemWarning ], ColorAfterFg: ConsoleColor.White, ColorAfterBg: ConsoleColor.Black, WaitForAcknowledgment: true );
                            Environment.Exit( 0 );
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the main menu options for the application, including transaction management, budget tools, reports,
        /// and options, while conditionally showing save and load options based on the auto-saving setting.
        /// </summary>
        /// <remarks>This method formats and outputs the menu items to the console, providing users with a
        /// clear set of choices. The display of save and load options is dependent on the application's auto-saving
        /// configuration to avoid user confusion.</remarks>
        private static void MainMenuChoice() {
            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.MainMenu_Header ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", MenuHeadings );
            ColorConsole.WriteLine( "\t1. " + messageOutput [ MessageEnum.MainMenu_TransactionManagement ], MenuItemColor, ResetColorAfter: false );
            ColorConsole.WriteLine( "\t2. " + messageOutput [ MessageEnum.MainMenu_BudgetTools ] );
            ColorConsole.WriteLine( "\t3. " + messageOutput [ MessageEnum.MainMenu_ReportsAndSummary ] );
            ColorConsole.WriteLine( "\t4. " + messageOutput [ MessageEnum.MainMenu_Options ] );
            // Only show save and load options if the user has turned off auto saving to prevent confusion, since these options are not needed when auto saving is on.
            if ( !saveOnEveryTransaction ) {
                ColorConsole.WriteLine( "\t5. " + messageOutput [ MessageEnum.MainMenu_Load ] );
                ColorConsole.WriteLine( "\t6. " + messageOutput [ MessageEnum.MainMenu_Save ] );
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} 1, 2, 3, 4, 5, {messageOutput [ MessageEnum.Label_Or ]} 6", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
            } else {
                // Print instructions for main menu options when load and save options are not present
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} 1, 2, 3, {messageOutput [ MessageEnum.Label_Or ]} 4", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
            }
        }
        #endregion

        #region >>> /// Budget Menu and related methods
        /// <summary>
        /// Prints an interactive screen where the user can update values for each catagory directly from their screen.
        /// The values are validated, stored and updated.
        /// Warrning displayed when the user's transactions are over 80% of the budget, or over 100% of the budget for the
        /// current calender month.
        /// </summary>
        /// <param name="dontClearOnDraw"></param>
        private static void BudgetMenu( bool dontClearOnDraw = false ) {
            int maxInputBoxBgColorLength = 17; //Length of input box for spacing warnings and consistancy
            //Allows the  submenus to return to the main Budget menu
            while ( true ) {
                //Allows Menus that returned here to leave Info at the top by not clearing. (maybe just have them clear after return instead)
                if ( !dontClearOnDraw ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                }
                int menuPadding = 8; //Padd between Ordered List Number and Transaction Category Label
                // Budget header
                Console.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.BudgetMenu_Header ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}" );
                #region >>> //Relative cursor position, padding and longest Cataegory Name, for formating
                (int Left, int Top) consolePosItem = Console.GetCursorPosition();
                consolePosItem.Top -= 2;
                consolePosItem.Left = transactionCategoryLongestSize + menuPadding + 1;
                #endregion 
                //Print the Catergory list for the budget method which extends function for printing input boxes.
                TransactionCatagoriesOrderedList( padding: menuPadding, maxInputBoxBgColorLength: maxInputBoxBgColorLength, usedForBudgetMethod: true );
                //Print instructions at bottom of list
                ColorConsole.Write( $"{messageOutput [ MessageEnum.BudgetMenu_SelectionInstruction ]} ", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                Console.WriteLine( messageOutput [ MessageEnum.BudgetMenu_WarningInstruction ] );
                Console.WriteLine();
                //Where the default column position should be for the cursor
                (int Left, int Top) consolePosRest = Console.GetCursorPosition();
                MessageFlagEnum msgFlag = MessageFlagEnum.NoMessage; //No message on start >> If there is an error with input display message
                //Main loop for printing budget
                while ( true ) {
                    //initiating cursor position within relative context
                    int cursorActivePos = 0;
                    //ConsoleKey range is relative to the key enum, this elemenates those gaps
                    int keyOffSet = 0;
                    ConsoleKeyInfo keyInfo = Console.ReadKey( intercept: true );
                    //Clears any messages when a user inputs data
                    if ( msgFlag > MessageFlagEnum.NoMessage ) {
                        Console.SetCursorPosition( consolePosRest.Left, consolePosRest.Top );
                        Console.Write( new string( ' ', Console.WindowWidth ) );
                        msgFlag = MessageFlagEnum.NoMessage;
                    }
                    //Sets the Key offset for 3 key ranges. Assumes fixed length enum for now.
                    //If updating for more categories or user defined categories change to a readLine format.
                    if ( keyInfo.Key > ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9 ) {
                        keyOffSet = keyInfo.Key - ConsoleKey.D0;
                        cursorActivePos = consolePosItem.Top + ( keyOffSet * 2 );
                    }
                    if ( keyInfo.Key > ConsoleKey.NumPad0 && keyInfo.Key <= ConsoleKey.NumPad9 ) {
                        keyOffSet = keyInfo.Key - ConsoleKey.NumPad0;
                        cursorActivePos = consolePosItem.Top + ( keyOffSet * 2 );
                    }
                    if ( keyInfo.Key >= ConsoleKey.A && keyInfo.Key < ConsoleKey.D ) {
                        keyOffSet = 10 + ( keyInfo.Key - ConsoleKey.A );
                        cursorActivePos = consolePosItem.Top + ( keyOffSet * 2 );
                    }
                    //Exit on escape or backspace
                    if ( keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Backspace ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        return;
                    }
                    //skip first item, since enum 0 is Income and not a transaction category to budget
                    if ( keyOffSet != 0 ) {
                        //Sets the Transaction category being updated. (Uses Keyinfo) And overwrite with the stored value.
                        TransactionCategory item = ( TransactionCategory ) keyOffSet;
                        //drawing the category Input Number
                        overWriteInput( saved_num: false, maxInputBoxBgColorLength: maxInputBoxBgColorLength );
                        //Initialize values needed for User to iput a number. (Based on ReadKey not ReadLine)
                        bool decimalUsed = false;
                        string numberToParse = "";
                        bool updateValue = false;
                        bool updatePrevious = false;
                        int maxLenOrg = 9; //Maximum length allowed for a number (if user uses a decimal allow extra 2.
                        int maxLen = maxLenOrg;
                        //Loop processing user input per key press
                        while ( true ) {
                            //Set up info for keys. future exapanding could just print the numberToParse from start position over and over.
                            //and just manipulate the string
                            ConsoleKeyInfo keyInfoWrite = Console.ReadKey( intercept: true );
                            ConsoleKey keyWrite = keyInfoWrite.Key;
                            var keyWriteChar = keyInfoWrite.KeyChar;
                            //User aborts updating the value
                            if ( keyWrite == ConsoleKey.Escape ) {
                                updateValue = false;
                                msgFlag = MessageFlagEnum.BudgetNotUpdated;
                                overWrite( $"{messageOutput [ MessageEnum.Label_Aborted ]} : {messageOutput [ MessageEnum.BudgetMenu_NotUpdated ]}", msgFlag );
                                Console.SetCursorPosition( consolePosRest.Left, consolePosRest.Top );
                                break;
                            }
                            //If user enters a number or a decimal just once
                            if ( char.IsNumber( keyWriteChar ) || ( keyWriteChar == '.' && decimalUsed == false ) ) {
                                //Only one period allowed, This is first to extend the 9 characters to 12 due to decimal and 2 extra digits for cents.
                                if ( keyWriteChar == '.' ) {
                                    decimalUsed = true;
                                    //Increase allowed length because formatted number without decimal will always add .00
                                    //extra will be rounded
                                    maxLen += 3;
                                }
                                //Checks if the number is within the allowed length before adding it to the string and printing it.
                                //Updates written value with a suffex padding for style.
                                if ( numberToParse.Length < maxLen ) {
                                    numberToParse += keyWriteChar.ToString();
                                    ColorConsole.Write( keyWriteChar.ToString(), colorByGroup [ ColorGroup.InputStyleA ] );
                                    ColorConsole.Write( " ", colorByGroup [ ColorGroup.InputStyleA ] );
                                    Console.CursorLeft -= 1;
                                } else {
                                    // print message that number is too large, but avoids reprinting if the message is alread
                                    if ( msgFlag != MessageFlagEnum.ExceedingInputSize ) {
                                        msgFlag = MessageFlagEnum.ExceedingInputSize;
                                        overWrite( $"{messageOutput [ MessageEnum.BudgetMenu_AmountExceeded ]} {Convert.ToDecimal( new string( '9', maxLen ) ) + 0.99M:C}", msgFlag );
                                        Console.SetCursorPosition( consolePosItem.Left + numberToParse.Length, cursorActivePos );
                                    }
                                }
                            }

                            //User Backspaces to delete characters, or enters to confirm number. Only works if there is something to delete or confirm.
                            if ( numberToParse.Length > 0 ) {
                                //Delete
                                if ( keyWrite == ConsoleKey.Backspace ) {
                                    //Will remove any messages at the bottom if they exist
                                    if ( msgFlag > MessageFlagEnum.NoMessage ) {
                                        Console.SetCursorPosition( consolePosRest.Left, consolePosRest.Top );
                                        Console.Write( new string( ' ', Console.WindowWidth ) );
                                        Console.SetCursorPosition( consolePosItem.Left + numberToParse.Length, cursorActivePos );
                                        msgFlag = MessageFlagEnum.NoMessage;
                                    }
                                    // Will allow decimal to be used again if deleted.
                                    if ( numberToParse.Last() == '.' ) {
                                        decimalUsed = false;
                                        //Now the max length should be back to no decimal. Compensastes because .00 added to Whole numbers
                                        maxLen = maxLenOrg;
                                    }
                                    // Clear Typed number using Cursot Left
                                    Console.CursorLeft -= 1;
                                    ColorConsole.Write( " ", colorByGroup [ ColorGroup.InputStyleA ] );
                                    Console.CursorLeft -= 1;
                                    //remove the last letter
                                    numberToParse = numberToParse.Substring( 0, numberToParse.Length - 1 );
                                }
                                //Final number confrimed by user
                                if ( keyWrite == ConsoleKey.Enter ) {
                                    msgFlag = MessageFlagEnum.BudgetUpdated;
                                    overWrite( $"{messageOutput [ MessageEnum.BudgetMenu_AmountAccepted ]} : {messageOutput [ MessageEnum.BudgetMenu_Updated ]}", msgFlag );
                                    updateValue = true; //Used for flaging value should be assined to Budget Dictionary values
                                    break;
                                }
                            } else {
                                if ( keyWrite == ConsoleKey.Backspace ) {
                                    //If user tries to backspace or enter with no value, print a message but avoid reprinting if message is already there.
                                    if ( msgFlag != MessageFlagEnum.InvalidNumber ) {
                                        msgFlag = MessageFlagEnum.InvalidNumber;
                                        overWrite( messageOutput [ MessageEnum.Warning_BadAmountNoZero ], msgFlag );
                                        Console.SetCursorPosition( consolePosItem.Left + numberToParse.Length, cursorActivePos );

                                    }
                                }
                                //Escape the input and updated back to origonal value if user tries to enter or backspace with no value to confirm or delete.
                                if ( keyWrite == ConsoleKey.Enter ) {
                                    updatePrevious = true;
                                    break;
                                }
                            }
                        }
                        //Validate users number they inputed, expected to always work due to prior sanitazation. This is just a defense
                        if ( updateValue || updatePrevious ) {
                            decimal updatedAmount = 0;
                            //Parse the decimal to be defensive and write the budget to file.
                            if ( decimal.TryParse( numberToParse, out updatedAmount ) && updateValue ) {
                                BudgetCategories [ item ] = updatedAmount;
                                WriteTransactionsAndBudget( BudgetOrTransaction.Budget );
                            } else {
                                msgFlag = MessageFlagEnum.InvalidNumber;
                                overWrite( $"{messageOutput [ MessageEnum.BudgetMenu_AmountInvalid ]} : {messageOutput [ MessageEnum.BudgetMenu_NotUpdated ]}", msgFlag );
                                Console.SetCursorPosition( consolePosRest.Left, consolePosRest.Top );
                            }
                        }
                        //Updates the item value with whatever was saved
                        overWriteInput( saved_num: true, maxInputBoxBgColorLength: maxInputBoxBgColorLength );


                        // Process if out of budget warrning should be displayed relative to the item updated
                        // the negative one compensates for the padding (Since pos is set up after padding to just start pputting in Value formated)
                        budgetWarning( item, consolePosItem.Left + maxInputBoxBgColorLength - 1, menuPadding );
                        //Moves cursor to write a message at the bottom and places back at the start of the budget item being editied

                        void overWrite( string msg, MessageFlagEnum type = MessageFlagEnum.NoMessage ) {
                            Console.SetCursorPosition( consolePosRest.Left, consolePosRest.Top );
                            if ( type == MessageFlagEnum.InvalidNumber || type == MessageFlagEnum.ExceedingInputSize ) {
                                ColorConsole.Write( $" {msg} ", colorByGroup [ ColorGroup.SystemError ] );
                            } else if ( type == MessageFlagEnum.BudgetUpdated ) {
                                ColorConsole.Write( $" {msg} ", colorByGroup [ ColorGroup.Success ] );
                            } else {
                                ColorConsole.Write( $" {msg}" );
                            }
                            Console.Write( new string( ' ', Console.WindowWidth - Console.CursorLeft ) );
                            Console.SetCursorPosition( consolePosItem.Left + numberToParse.Length, cursorActivePos );
                        }
                        //Clears from relative Cursor Position or:
                        //Prints a value stored in memory. Used for initializing Budget screen or if the user escapes input (restores value)
                        void overWriteInput( bool saved_num = true, int maxInputBoxBgColorLength = 0 ) {
                            //Note about PaddingLength 
                            Console.CursorLeft = consolePosItem.Left;
                            Console.CursorTop = cursorActivePos;
                            //Adds extra spaces to keep the input box uniform
                            int testformLength = BudgetCategories [ item ].ToString( "C" ).Length;
                            string fillInput = new( ' ', maxInputBoxBgColorLength - testformLength - 1 );
                            int test = fillInput.Length;
                            //Print the value typed in formatted, or load the saved number from the budget.
                            //Not printing a value it fills as a full blank input based on maxInputBoxBgColorLength (minus one because padding ' ' added beforehand)
                            string printVal = saved_num ? BudgetCategories [ item ].ToString( "C" ) + fillInput : new string( ' ', maxInputBoxBgColorLength - 1 );
                            ColorConsole.Write( printVal, colorByGroup [ ColorGroup.InputStyleA ] );
                            //White Space to clear the window 
                            ColorConsole.Write( new string( ' ', Console.WindowWidth - ( consolePosItem.Left + maxInputBoxBgColorLength ) ) ); //-2 is for padding
                            Console.CursorLeft = consolePosItem.Left; //Reset cursor position to relative position
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Displays the wornning for the budget categories in a relative position
        /// </summary>
        /// <param name="category">Category warning is being assesed for</param>
        /// <param name="consolePosItem">Position of the Console.Cursor top</param>
        /// <param name="padding">Padding that was used by the Budget menu List</param>
        /// <param name="afterInitial">Specify weather this is the first draw of the Budgetmenu or if called while items updated</param>
        private static void budgetWarning( TransactionCategory category, int consolePosItemLeft, int padding, bool afterInitial = true ) {
            //needs checking
            var today = DateOnly.FromDateTime( DateTime.Today ); //Used to geth todays Month and Year for budget comparison
            List<Transaction> transforCat = new();
            //Query all transactions within present month and produce a sum
            transforCat = Transactions
                            .Where( trans =>
                                    trans.Date.Month == today.Month &&
                                    trans.Date.Year == today.Year &&
                                    trans.Category == category ).ToList();
            decimal total = transforCat.Sum( t => t.Amount );
            //Transactions over 100%
            if ( total > 1m * BudgetCategories [ category ] ) {
                Console.CursorLeft = consolePosItemLeft + 1;
                ColorConsole.Write( $" << {messageOutput [ MessageEnum.BudgetMenu_BudgetExceeded ]} ( 100% ) {messageOutput [ MessageEnum.Label_Total ]} : {total} ", colorByGroup [ ColorGroup.SystemError ] );
            } else if ( total > 0.8m * BudgetCategories [ category ] ) {
                Console.CursorLeft = consolePosItemLeft + 1;
                ColorConsole.Write( $" << {messageOutput [ MessageEnum.BudgetMenu_BudgetExceeded ]} ( 80% ) {messageOutput [ MessageEnum.Label_Total ]} : {total} ", colorByGroup [ ColorGroup.SystemError ] );
                ColorConsole.Write( new string( ' ', Console.WindowWidth - 3 - transactionCategoryLongestSize - padding - 18 ), colorByGroup [ ColorGroup.Default ] );
            }
        }
        #endregion

        #region >>> /// Transaction Management Menu and methods
        /// <summary>
        /// Transaction Management Menu for making choices.
        /// </summary>
        private static void TransactionManagementMenu( bool dontClearOnDraw = false ) {
            while ( true ) {
                //Clear the screen weather returning or from a different menu (Main Menu)
                if ( !dontClearOnDraw ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                }
                TransactionManagementMenuChoice();
                switch ( Console.ReadKey( intercept: true ).Key ) {
                    //Add Income
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddIncomeExpenseTransaction( IncomeOrExpense.Income );
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        AddIncomeExpenseTransaction( IncomeOrExpense.Expense );
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        if ( Transactions.Count == 0 ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.System_NoReleventTransactions ]} ", colorByGroup [ ColorGroup.SystemError ] );
                            AnyKeyToContinue( true );

                        } else {
                            ViewTransactions( Transactions, rearrangeList: true );
                        }
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        if ( Transactions.Count == 0 ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.System_NoReleventTransactions ]} ", colorByGroup [ ColorGroup.SystemError ] );
                            AnyKeyToContinue( true );
                        } else
                            SearchByTransactions(); //Uses view transaction
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Backspace:
                        return;
                }
            }
        }

        /// <summary>
        /// Display an ordered list of the Transaction Catagories. List numbers to be used for ReadKey
        /// </summary>
        /// <param name="padding">Padding used to space out the Ordered List Number</param>
        private static void TransactionCatagoriesOrderedList( int padding, int maxInputBoxBgColorLength = 0, bool usedForBudgetMethod = false, bool viewByCatagory = false ) {
            bool start = false;
            //Print menu
            char ConsoleKeychar = '1'; //List stars with first key, 
            if ( !viewByCatagory )
                start = true; //Allows skipping enum 0 with foreach
            foreach ( TransactionCategory cat in Enum.GetValues( typeof( TransactionCategory ) ) ) {
                //Skip first enum (Should be Income)
                if ( !start ) {
                    if ( ConsoleKeychar == '9' + 1 )
                        ConsoleKeychar = 'A';
                    //Prints Transaction Category list number
                    ColorConsole.Write( ConsoleKeychar.ToString(), colorByGroup [ ColorGroup.MenuItems ] );
                    ColorConsole.Write( ". " + messageOutput [ TransactionCategoryToLanguage [ cat ] ], colorByGroup [ ColorGroup.MenuItems ] );
                    if ( !usedForBudgetMethod )
                        //This needs to be used to reset the color after
                        Console.WriteLine( "" );
                    if ( usedForBudgetMethod ) {
                        Console.CursorLeft = transactionCategoryLongestSize + padding;
                        //Prints Transaction Category with padding.
                        ColorConsole.Write( " ", colorByGroup [ ColorGroup.InputStyleA ], ResetColorAfter: false );
                        //Writes the Budget VALUE
                        Console.Write( BudgetCategories [ cat ].ToString( "C" ) );
                        //Pads the end with spaces so input boxes are the same size with small or large numbers
                        Console.Write( new string( ' ', maxInputBoxBgColorLength - BudgetCategories [ cat ].ToString( "C" ).Length - 1 ) );
                        budgetWarning( cat, Console.GetCursorPosition().Left, padding, afterInitial: false );
                        ColorConsole.WriteLine( "\n", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
                    }
                    ConsoleKeychar++;
                } else start = false; //Allow run aftering skipping first element
            }
        }
        /// <summary>
        /// Displays the transaction management menu options to the user, including options to add income, add expense,
        /// view all transactions, and search transactions.
        /// All transactions currently use $, future uption to choose currency should be considered.
        /// </summary>
        /// <remarks>This method is responsible for rendering the menu interface for transaction
        /// management, guiding the user on available actions. It also provides instructions for navigating the
        /// menu.</remarks>
        private static void TransactionManagementMenuChoice() {
            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.MainMenu_TransactionManagement ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} ", MenuHeadings );
            ColorConsole.WriteLine( "\t1. " + messageOutput [ MessageEnum.TransMgnt_AddIncomeTransaction ], MenuItemColor, ResetColorAfter: false );
            ColorConsole.WriteLine( "\t2. " + messageOutput [ MessageEnum.TransMgnt_AddExpenseTransaction ] );
            ColorConsole.WriteLine( "\t3. " + messageOutput [ MessageEnum.TransMgnt_ViewAllTransactions ] );
            ColorConsole.WriteLine( "\t4. " + messageOutput [ MessageEnum.TransMgnt_SearchTransactions ] );
            ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
            ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} 1, 2, 3, {messageOutput [ MessageEnum.Label_Or ]} 4", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
        }
        /// <summary>
        /// Search Transactions allows the user to search by dates, amounts or categories. Additionaly they can specify two conditions:
        /// Order by ascending or descening and if they want the printout to be color banded rows.
        /// This method calls the Viewtransaction methods passing it a reordered list filtered. And the conditions for banding
        /// presentation are passed.
        /// </summary>
        private static void SearchByTransactions() {
            while ( true ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                SearchType searchby = SearchType.Category; //sets the default
                ColorConsole.WriteLine( messageOutput [ MessageEnum.SrcByTrans_HeaderQuestion ], colorByGroup [ ColorGroup.Header ] );
                Console.WriteLine();
                ColorConsole.WriteLine( $"1. {messageOutput [ MessageEnum.SrcByTrans_Category ]}", colorByGroup [ ColorGroup.MenuItems ], ResetColorAfter: false );
                ColorConsole.WriteLine( $"2. {messageOutput [ MessageEnum.SrcByTrans_DateRange ]}" );
                ColorConsole.WriteLine( $"3. {messageOutput [ MessageEnum.SrcByTrans_PriceRange ]}" );
                ColorConsole.WriteLine( "", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
                Console.WriteLine();
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Label_Options ]}", colorByGroup [ ColorGroup.Header ] );
                Console.WriteLine();
                ColorConsole.WriteLine( $"A. {messageOutput [ MessageEnum.SrcByTrans_OptionOrderAsc ]}", colorByGroup [ ColorGroup.MenuItems ] );
                ColorConsole.WriteLine( $"B. {messageOutput [ MessageEnum.SrcByTrans_OptionOrderDesc ]}", colorByGroup [ ColorGroup.MenuItems ] );
                ColorConsole.WriteLine( $"C. {messageOutput [ MessageEnum.SrcByTrans_OptionTableColorBanding ]}", colorByGroup [ ColorGroup.MenuItems ] );
                Console.WriteLine();
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]}: 1, 2, {messageOutput [ MessageEnum.Label_Or ]} 3 : {messageOutput [ MessageEnum.SrcByTrans_OptionApplied ]} : ", ConsoleColor.White );
                int cursorPos = Console.CursorLeft; //This stores the osition for the flags to write
                Console.Write( "A | C" );
                int [ ] OptionPosInListOrder = [ 0, 0, 4 ]; //Order of each position, A and B are toggle so same spot
                int endindex = 6; //just the end of the leter indexs. 5 + addin a space
                                  //Default prefrences
                bool toggleA = true; //means A or B, false is B
                bool toggleBDecending = false;
                bool toggleCColorBand = true;
                //Input logic, loops until input received
                while ( true ) {
                    //preffered if statement for this
                    ConsoleKeyInfo key = Console.ReadKey( intercept: true );
                    //User choose to exit
                    if ( key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Escape ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_SearchAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ThreadSleepAndClearKeys( 500, clearScreen: true );
                        return;
                    }
                    if ( key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1 )
                        break;
                    if ( key.Key == ConsoleKey.NumPad2 || key.Key == ConsoleKey.D2 ) {
                        searchby = SearchType.DateRange;
                        break;
                    }
                    if ( key.Key == ConsoleKey.NumPad3 || key.Key == ConsoleKey.D3 ) {
                        searchby = SearchType.PriceRange;
                        break;
                    }
                    //Toggle order results accending
                    if ( key.Key == ConsoleKey.A ) {
                        if ( toggleBDecending ) {
                            toggleBDecending = false;
                            Console.CursorLeft = cursorPos;
                        }
                        if ( !toggleA ) {
                            toggleA = true;
                            Console.Write( "A" );
                        }
                        Console.CursorLeft = cursorPos + endindex;
                    }
                    //Toggle order results descending
                    if ( key.Key == ConsoleKey.B ) {
                        if ( toggleA ) {
                            toggleA = false;
                            Console.CursorLeft = cursorPos;
                        }
                        if ( !toggleBDecending ) {
                            toggleBDecending = true;
                            Console.Write( "B" );
                        }
                        Console.CursorLeft = cursorPos + endindex;
                    }
                    //Toggle banded background row colors. (alternating backgrounds)
                    if ( key.Key == ConsoleKey.C ) {
                        toggleCColorBand = !toggleCColorBand;
                        if ( toggleCColorBand ) {
                            Console.CursorLeft = cursorPos + OptionPosInListOrder [ 2 ];
                            Console.Write( "C" );
                        } else {
                            Console.CursorLeft = cursorPos + OptionPosInListOrder [ 2 ];
                            Console.Write( " " );
                        }
                        Console.CursorLeft = cursorPos + endindex;
                    }
                }
                //Logical portion
                Console.Write( "\x1b[3J" );
                Console.Clear();
                //Create empty list for newly organized items
                List<Transaction> orderedTrans;
                switch ( searchby ) {
                    case SearchType.PriceRange:
                        while ( true ) {
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.SrcByTrans_EnterAmount1 ], colorByGroup [ ColorGroup.Header ] );
                            Console.WriteLine();
                            decimal amt1 = 0m;
                            decimal amt2 = 0m;
                            if ( !GetAmount( ref amt1, allowInputZero: true ) )
                                break;
                            Console.Write( "\x1b[3J" );
                            Console.Clear();
                            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.SrcByTrans_FirstAmountIs ]} : " + amt1.ToString( "C" ), colorByGroup [ ColorGroup.Header ] );
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.SrcByTrans_EnterAmount2 ], colorByGroup [ ColorGroup.Header ] );
                            if ( !GetAmount( ref amt2, allowInputZero: false ) )
                                break;
                            //Keeping the order from low to high. Instead of BETWEEN which im not sure LINQ supports
                            if ( amt1 > amt2 ) {
                                decimal amtTemp = amt1;
                                amt1 = amt2;
                                amt2 = amtTemp;
                            }
                            //AMOUNT >> Trigger order by decending, otherwise order by ascending
                            if ( toggleBDecending ) {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Amount >= amt1 && trans.Amount <= amt2 // << not sure how to use as variable
                                                 orderby trans.Amount descending //<<<<
                                                 select trans ).ToList();
                            } else {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Amount >= amt1 && trans.Amount <= amt2 // << not sure how to use as variable
                                                 orderby trans.Amount ascending
                                                 select trans ).ToList();
                            }
                            if ( orderedTrans.Count > 0 ) {
                                if ( !ViewTransactions( orderedTrans, $"({messageOutput [ MessageEnum.SrcByTrans_PriceRange ]})", colorBand: toggleCColorBand ) )
                                    break;
                            } else {
                                Console.WriteLine( messageOutput [ MessageEnum.SrcByTrans_NoResultSrcAgain ] );
                                if ( !trySearch() )
                                    break;
                                else
                                    continue;
                            }
                            break;
                        }
                        break;
                    case SearchType.Category:
                        while ( true ) {
                            TransactionCategory cat = new();
                            //Calling another function that prints the menu, thus bypassing some detials 
                            // Also this checks if the belo returns no category
                            if ( !GetCategory( ref cat, usedForBudgetMethod: false, viewAllCatergories: true ) ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.SrcByTrans_SrcAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                                ThreadSleepAndClearKeys( 500, clearScreen: true );
                                break;
                            }
                            // CATEGORY Trigger order by decending, otherwise order by ascending 
                            if ( toggleBDecending ) {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Category == cat // << not sure how to use as variable
                                                 orderby trans.Date ascending
                                                 select trans ).ToList();
                            } else {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Category == cat // << not sure how to use as variable
                                                 orderby trans.Date descending
                                                 select trans ).ToList();
                            }
                            //Weather there are orders or not
                            if ( orderedTrans.Count > 0 ) {
                                //if the user abords exit. (ViewTransactions Returns true
                                if ( !ViewTransactions( orderedTrans, $"({messageOutput [ MessageEnum.SrcByTrans_Category ]})", colorBand: toggleCColorBand ) ) {
                                    return;
                                }
                            } else {
                                Console.WriteLine( messageOutput [ MessageEnum.SrcByTrans_NoResultSrcAgain ] );
                                if ( !trySearch() )
                                    break;
                                else
                                    continue;
                            }
                            break;
                        }
                        break;
                    case SearchType.DateRange:
                        while ( true ) {
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.SrcByTrans_EnterDate1 ], colorByGroup [ ColorGroup.Header ] );
                            Console.WriteLine();
                            DateOnly date1 = new();
                            DateOnly date2 = new();
                            if ( !GetDate( ref date1 ) ) {
                                break;
                            }
                            Console.Write( "\x1b[3J" );
                            Console.Clear();
                            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.SrcByTrans_FirstDateIs ]} : " + date1.ToString( dateFormatOut.Item1 ), colorByGroup [ ColorGroup.Header ] );
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.SrcByTrans_EnterDate2 ], colorByGroup [ ColorGroup.Header ] );

                            if ( !GetDate( ref date2, compareDate: ref date1 ) ) {
                                break;
                            }
                            // DATE RANGE -  Trigger order by decending, otherwise order by ascending
                            if ( toggleBDecending ) {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Date >= date1 && trans.Date <= date2 // << not sure how to use as variable
                                                 orderby trans.Date ascending
                                                 select trans ).ToList();
                            } else {
                                orderedTrans = ( from trans in Transactions
                                                 where trans.Date >= date1 && trans.Date <= date2 // << not sure how to use as variable
                                                 orderby trans.Date descending
                                                 select trans ).ToList();
                            }
                            if ( orderedTrans.Count > 0 )
                                ViewTransactions( orderedTrans, $"({messageOutput [ MessageEnum.SrcByTrans_DateRange ]})", colorBand: toggleCColorBand );
                            else {
                                Console.WriteLine( messageOutput [ MessageEnum.SrcByTrans_NoResultSrcAgain ] );
                                if ( !trySearch() )
                                    break;
                                else
                                    continue;
                            }
                            break;
                        }
                        break;
                }
                bool trySearch() {
                    ColorConsole.Write( $"\n{messageOutput [ MessageEnum.Label_Press ]} : ({messageOutput [ MessageEnum.SystemInstructions_SpaceOrEnter ]})", colorByGroup [ ColorGroup.MenuItems ] );
                    ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Or ].ToUpper()} ", ConsoleColor.White );
                    ColorConsole.WriteLine( $"({messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                    while ( true ) {
                        ConsoleKey key = Console.ReadKey( intercept: true ).Key;
                        if ( key == ConsoleKey.Spacebar || key == ConsoleKey.Enter ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            return true;
                        }
                        if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                            ThreadSleepAndClearKeys( 500, clearScreen: true );
                            return false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// View all transactions from a provided list. 
        /// Prompt user for Multiple viewing modes: Giant List, Pages, Both (Pages that don't clear the screen!)
        /// User navigates the Pages mode by using several keyboard shortcuts for back and forth.
        /// Banding is supported to make viewing esier.
        /// THIS NEEDS SOME WORK TO ADD checking character code ranges that register as multi column characters and adjust/trucate to fit!
        /// </summary>
        /// <param name="passedTransactions">List holding the transactions to be viewed</param>
        /// <param name="Label">Manu label suffex to specify a fine detail such as noting the different types such as "expense" or "income"</param>
        /// <param name="rearrangeList">Default you can sort by date, incase you want to feed it an unordered list dont specify anything</param>
        /// <param name="colorBand">True if you want results to have alternating background colors for each row</param>
        /// <returns></returns>
        private static bool ViewTransactions( List<Transaction> passedTransactions, string Label = "", bool rearrangeList = false, bool colorBand = true ) {
            //adds spacing if a label is added if not don't take any extra space
            Label = string.IsNullOrEmpty( Label ) ? "" : System.String.Concat( " ", Label );
            ScrollType scroller = ScrollType.Pager; //Default Display Mode (my favourite)
            Console.Write( "\x1b[3J" ); Console.Clear();
            //No transactions to load
            if ( passedTransactions.Count == 0 ) {
                ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.System_NoReleventTransactions ]} ", colorByGroup [ ColorGroup.SystemError ] );
                AnyKeyToContinue( true );
            }
            //Print the instructions
            ColorConsole.WriteLine( messageOutput [ MessageEnum.ReportAndSum_AskHowToView ], colorByGroup [ ColorGroup.Header ] );
            Console.WriteLine();
            ColorConsole.WriteLine( $"1. {messageOutput [ MessageEnum.ReportAndSum_Scroll ]}", colorByGroup [ ColorGroup.MenuItems ] );
            ColorConsole.WriteLine( $"2. {messageOutput [ MessageEnum.ReportAndSum_Pages ]}", colorByGroup [ ColorGroup.MenuItems ] );
            ColorConsole.WriteLine( $"3. {messageOutput [ MessageEnum.ReportAndSum_PageAndScrollNoClear ]}", colorByGroup [ ColorGroup.MenuItems ] );
            Console.WriteLine();
            ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
            ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} : 1, 2, {messageOutput [ MessageEnum.Label_Or ]} 3 ", ConsoleColor.White );
            //Process user input
            while ( true ) {
                ConsoleKeyInfo key = Console.ReadKey( intercept: true );
                //User choose to exit
                if ( key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Escape )
                    return false;
                if ( key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1 ) {
                    scroller = ScrollType.Scroller;
                    break;
                }
                if ( key.Key == ConsoleKey.NumPad2 || key.Key == ConsoleKey.D2 )
                    break;//default initiatated at top
                if ( key.Key == ConsoleKey.NumPad3 || key.Key == ConsoleKey.D3 ) {
                    scroller = ScrollType.Both;
                    break;
                }
            }
            //Told chang gpt what I wanted for this snipit, I assumed I didnt want to write to the screen
            //like I did in the transaction print out, but I guess thats the best way.
            //Prvents mismatch in character column widths
            int maxLenCat = 0;
            Console.Write( "\x1b[3J" ); Console.Clear();
            ConsoleColor restoreForeground = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            foreach ( var kv in TransactionCategoryToLanguage ) {
                string text = messageOutput [ kv.Value ];
                int start = Console.CursorLeft;
                Console.Write( text );
                int width = Console.CursorLeft - start;
                Console.CursorLeft = start; // reset cursor
                if ( width > maxLenCat )
                    maxLenCat = width;
            }
            Console.ForegroundColor = restoreForeground;
            Console.Write( "\x1b[3J" ); Console.Clear();
            //Sort by date into new list
            List<Transaction> SortedTransactions = passedTransactions;
            if ( rearrangeList )
                SortedTransactions.Sort( ( s1, s2 ) => s1.Date.CompareTo( s2.Date ) );
            //for checking length
            int maxLenAmount = 6;

            //May appear unnesscary, but if we change setting in other part of program for long dates we will still space out properly.
            int maxLenDate = dateFormatOut.Item2; //2nd item in touple is integer of the length. Using long names returns september as longest
            //A better approach would be to check length with every added item and store it, and only do this once on file load.
            foreach ( Transaction transaction in SortedTransactions ) {
                //Formats as currency then captures the longest one
                int amountLen = transaction.Amount.ToString( "C" ).Length;
                int catLen = Enum.GetName( transaction.Category ).Length;
                //Increases if Mac amount is smaller
                if ( maxLenAmount < amountLen )
                    maxLenAmount = amountLen;
            }
            //This is relevent for languages like chinese where the word for Amount may be longer. (especially, longer than longest transaction)

            //height of the window before printing for overflow
            int bufHeight = Console.WindowHeight;
            int pgCount = 1;
            int nonitemLines = 4;
            int pgMax = SortedTransactions.Count / ( bufHeight - nonitemLines );
            int pgRemainder = SortedTransactions.Count % ( bufHeight - nonitemLines );
            if ( pgRemainder > 0 )
                pgMax++;
            if ( scroller == ScrollType.Scroller )
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.TransMgnt_ViewAllTransactions ]}{Label} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.Header ] );
            else
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.TransMgnt_ViewAllTransactions ]}{Label} ( {messageOutput [ MessageEnum.Label_PageAbreviated ]} : {pgCount}/{pgMax} ) {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.Header ] );            //Length of Labels (based on language)
            int dateLabelLen = messageOutput [ MessageEnum.Label_Date ].ToString().Length;
            int amtLabelLen = messageOutput [ MessageEnum.Label_Amount ].ToString().Length;
            int descLabelLen = messageOutput [ MessageEnum.Label_Description ].ToString().Length;
            int catLabelLen = messageOutput [ MessageEnum.Label_Category ].ToString().Length;
            //Length of batting between items, later write line will use something like " | " 3 per item-1 = 9;
            string bodySpacer = "|  ";
            int headerSpacer = bodySpacer.Length + 2;
            int totalPaddingLength = headerSpacer * 3;
            //Overall padding available after longes printed items considered. (ie if the descriptions are short, or the amounts etc)
            int maxLenDesc = Console.WindowWidth - ( maxLenAmount + maxLenCat + maxLenDate + totalPaddingLength );
            //Sepcify header padding lengths //Amout is last so padding not required
            int catLeftPos = maxLenDate + headerSpacer;
            int descLeftPos = maxLenCat + catLeftPos + headerSpacer;
            int amtLeftPos = maxLenDesc + descLeftPos + headerSpacer;
            Writeheader();
            //Writes each transation formated with currency
            int lineCount = 1; //tracks how much each page held. used to set start index for each subequent page
            int evenodd = 0; //for color banding
            int i = 0;
            //Tracks each pages linecount printed, and length is the number of pages.
            int [ ] index_start_page = new int [ pgMax + 1 ];

            while ( true ) {
                //First line save index for Pager to track when moving back to this page. (if this page was 21, and when coming back, system knows to start printing 21)
                if ( lineCount == 1 )
                    index_start_page [ pgCount - 1 ] = i;
                //SCROLLER - Exit when whole list is printed and using scrolling tpye
                if ( i > SortedTransactions.Count - 1 && scroller == ScrollType.Scroller ) {
                    Console.ForegroundColor = colorByGroup [ ColorGroup.SystemInstructionsGray ] [ 0 ];
                    AnyKeyToContinue( msg: $"({messageOutput [ MessageEnum.Label_Press ]} {messageOutput [ MessageEnum.System_AnyKeyToExit ]})" );
                    // Use the ANSI escape sequence to clear the scrollback buffer
                    // Optional: Call Console.Clear() again to ensure a completely clean start, 
                    // as the escape sequence might leave the cursor on the second line.
                    Console.Write( "\x1b[3J" ); Console.Clear();

                    return true;
                }
                Transaction transaction = SortedTransactions.ElementAt( i ); //Get Transaction to print
                //Optional color banding. Defualt is active, otherwise use green
                if ( colorBand ) {
                    Console.ForegroundColor = evenodd == 0 ? ConsoleColor.Green : ConsoleColor.Cyan;
                    evenodd = ( evenodd == 0 ) ? 1 : 0;
                } else
                    if ( i == 0 )
                        Console.ForegroundColor = ConsoleColor.Green; //dont reset over and over if color banding is not on, it will already be green
                //Printing out Details in single row
                #region///Print Date Formated
                Console.Write( transaction.Date.ToString( dateFormatOut.Item1 ) );
                Console.Write( new string( ' ', maxLenDate - transaction.Date.ToString( dateFormatOut.Item1 ).Length ) + "  " + bodySpacer ); //sapcer
                #endregion
                #region ///Print Category        - Add spaces if not the max size, print spacer 3 chars
                Console.CursorLeft = catLeftPos;
                Console.Write( messageOutput [ TransactionCategoryToLanguage [ transaction.Category ] ] );
                //These no longer work due to chinese characters taking wide spaces
                //Max length - the actual legnth+ 3 for padding. So if the longest word is 12, and we have gas (3), should fill 9 spaces + 3 spaces.
                //if ( ( maxLenCat - Enum.GetName( transaction.Category ).Length ) > 0 )
                //    Console.Write( new string( ' ', maxLenCat - Enum.GetName( transaction.Category ).Length ) );
                //Console.Write( bodySpacer );
                Console.CursorLeft = descLeftPos - 3;
                Console.Write( bodySpacer );
                #endregion
                #region ///Print Description
                string descOutTrunc = transaction.Description;
                ///This method truncated strings that are too long, but some character mess up with length and cell width mismatch
                //if ( transaction.Description.ToString().Length > maxLenDesc - 3 ) {
                //    descOutTrunc = descOutTrunc.Substring( 0, maxLenDesc - 3 );
                //    Console.Write( descOutTrunc + "..." );
                //} else
                //    Console.Write( descOutTrunc );

                foreach ( char item in descOutTrunc ) {
                    if ( Console.CursorLeft > ( amtLeftPos - 9 ) ) {
                        Console.CursorLeft = amtLeftPos - 8;
                        Console.Write( "..." );
                        break;
                    } else {
                        Console.Write( item );
                    }

                }



                Console.CursorLeft = amtLeftPos - 3;


                Console.Write( bodySpacer );


                #endregion
                #region ///Print Amount
                Console.WriteLine( transaction.Amount.ToString( "C" ) );
                #endregion
                //Controll page changing
                if ( scroller == ScrollType.Pager || scroller == ScrollType.Both ) {
                    //Last Transaction on las page Printed
                    if ( pgCount == pgMax && i == SortedTransactions.Count - 1 ) {
                        //Sets the position to print this always at the bottom for consistancy
                        Console.CursorTop = Console.WindowHeight - 1;
                        ColorConsole.Write( messageOutput [ MessageEnum.SystemInstructions_PageView ], colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                        //Update remainder to normalize the 0 in modulus
                        int remainder = ( pgRemainder == 0 ) ? ( bufHeight - nonitemLines ) : pgRemainder;
                        int toTop = 0;
                        //Navigation controls for the last page
                        while ( true ) {
                            ConsoleKeyInfo key = Console.ReadKey( true );
                            //Go back a page as long as its not page 1!
                            if ( pgMax != 1 && ( key.Key == ConsoleKey.PageUp || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.UpArrow ) ) {
                                pgCount--;
                                lineCount = 1;
                                i = index_start_page [ pgCount - 1 ];
                                toTop = 1;
                                //Clears if pager, but drops line for both because the instructions use Write which would allow menu on the same line
                                if ( scroller == ScrollType.Pager ) {
                                    Console.Write( "\x1b[3J" ); Console.Clear();
                                } else
                                    Console.WriteLine();
                                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.TransMgnt_ViewAllTransactions ]}{Label} ({messageOutput [ MessageEnum.Label_PageAbreviated ]} : {pgCount}/{pgMax} ) {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.Header ] );
                                Writeheader();
                                break;
                            }
                            //exit by command
                            if ( key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Q ) {
                                toTop = 2;
                                break;
                            }
                        }
                        if ( toTop == 2 )
                            break;
                        if ( toTop == 1 )
                            continue;
                    }
                    #region ///Each Page finished except the last
                    if ( lineCount == bufHeight - nonitemLines ) {       //Screen full reset and print
                        lineCount = 0;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        ColorConsole.Write( messageOutput [ MessageEnum.SystemInstructions_PageView ], colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                        int toTop = 0; //Used to track itteration of master for loop. (this scope)
                        while ( toTop == 0 ) {
                            ConsoleKeyInfo key = Console.ReadKey( true );
                            //Not first page (Can go back a page)
                            if ( pgCount > 1 && ( key.Key == ConsoleKey.PageUp || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.UpArrow ) ) {
                                pgCount--;
                                lineCount = 1;
                                i = index_start_page [ pgCount - 1 ];
                                toTop = 1;
                            }
                            if ( ( pgCount < pgMax && key.Key == ConsoleKey.PageDown ) || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.DownArrow ) {
                                pgCount++;
                                break; //bypass toTop control to print header
                            }
                            if ( key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Q )
                                toTop = 2;
                        }
                        //Clears if pager, but drops line for both because the instructions use Write which would allow menu on the same line
                        if ( scroller == ScrollType.Pager ) {
                            Console.Write( "\x1b[3J" ); Console.Write( "\x1b[3J" );
                        } else {
                            Console.WriteLine();
                        }
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.TransMgnt_ViewAllTransactions ]}{Label} ({messageOutput [ MessageEnum.Label_PageAbreviated ]} : {pgCount}/{pgMax} ) {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.Header ] );
                        Writeheader();
                        //Back to loop start with updated variables
                        if ( toTop == 1 ) {
                            toTop = 0;
                            continue;
                        }
                        //Exit
                        if ( toTop == 2 )
                            break;
                    }
                    #endregion
                }
                lineCount++;
                i++;
            }
            void Writeheader() {
                //Write the header
                ColorConsole.Write( messageOutput [ MessageEnum.Label_Date ], ConsoleColor.Red, ResetColorAfter: false );
                Console.CursorLeft = catLeftPos;
                Console.Write( messageOutput [ MessageEnum.Label_Category ] );
                Console.CursorLeft = descLeftPos;
                Console.Write( messageOutput [ MessageEnum.Label_Description ] );
                Console.CursorLeft = amtLeftPos;
                Console.WriteLine( messageOutput [ MessageEnum.Label_Amount ] );
                Console.WriteLine();
            }
            return false;
        }
        /// <summary>
        /// Used to add Income or expenses to the transaction list. User is able to abort at any time.
        /// Requires specifying if adding Expense or Income. Expense adds user prompt for transaction catagory.
        /// </summary>
        /// <param name="type">Specify if adding Income o Expense</param>
        /// <returns>A bool of weather to clear parent screen or not (may not be used at this point)</returns>
        private static void AddIncomeExpenseTransaction( IncomeOrExpense type ) {
            string transType;
            Console.Write( "\x1b[3J" ); Console.Clear();
            decimal amount = 0m;
            DateOnly date = new();
            string descriptionInput;
            TransactionCategory cat = new();
            //Print header
            if ( type == IncomeOrExpense.Expense ) {
                transType = messageOutput [ MessageEnum.Label_Expense ];
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_AddExpenseTransaction ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", MenuHeadings );
            } else {
                transType = messageOutput [ MessageEnum.Label_Income ];
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_AddIncomeTransaction ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", MenuHeadings );
            }
            Console.WriteLine();
            //Ask Date
            if ( !GetDate( ref date ) ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                ThreadSleepAndClearKeys( 500, clearScreen: true );
                return;
            }
            if ( type == IncomeOrExpense.Expense ) {
                //Calling another function that prints the options of the categories
                if ( !GetCategory( ref cat, usedForBudgetMethod: false ) ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                    ThreadSleepAndClearKeys( 500, clearScreen: true );
                    return;
                }
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.TransMgnt_AddingExpenseFor ]} : {Enum.GetName( cat )}", colorByGroup [ ColorGroup.MenuItems ] );
                Console.WriteLine();
            }
            //Ask Description
            while ( true ) {
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_Abort ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.SystemInstructions_InputTransDescription ]} : ", colorByGroup [ ColorGroup.SystemPromptInstructions ] );
                (int cursorLeft, int cursorTop) = Console.GetCursorPosition();
                descriptionInput = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] );
                // user typed exit in native language, returning
                if ( descriptionInput == messageOutput [ MessageEnum.Label_Exit ] ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                    ThreadSleepAndClearKeys( 500 );
                    return;
                }
                // Changes null, whitespace, or empty to a defualt, resets cursor to write it as if it was inputed.
                if ( string.IsNullOrWhiteSpace( descriptionInput ) ) {
                    descriptionInput = messageOutput [ MessageEnum.TransMgnt_NoDscProvided ];
                    Console.SetCursorPosition( cursorLeft, cursorTop );
                    ColorConsole.WriteLine( descriptionInput, colorByGroup [ ColorGroup.InputStyleText ] );
                }
                break;
            }
            //Ask amount
            if ( !GetAmount( ref amount ) ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                ThreadSleepAndClearKeys( 500, clearScreen: true );
                return;
            }
            //create new transaction and display it, wait for user to confirm before going back to transaction management menu
            //Also saves the transactions to the file on each update.
            if ( type == IncomeOrExpense.Expense )
                Transactions.Add( new Transaction( date, amount, descriptionInput, cat ) );
            else
                Transactions.Add( new Transaction( date, amount, descriptionInput ) );
            Console.Write( "\x1b[3J" ); Console.Clear();
            //Print Labels
            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.TransMgnt_TransactionAdded ]} : {transType} ", colorByGroup [ ColorGroup.Success ] );
            Console.WriteLine();
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_Date ]} : ", colorByGroup [ ColorGroup.SystemInstructions ] );
            Console.WriteLine( $"{Transactions.Last().Date}, " );
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_Amount ]} : ", colorByGroup [ ColorGroup.SystemInstructions ] );
            Console.WriteLine( $"{Transactions.Last().Amount.ToString( "C" )}, " ); //formats as currency of the system.
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_Description ]} : ", colorByGroup [ ColorGroup.SystemInstructions ] );
            ColorConsole.WriteLine( $"{Transactions.Last().Description}" );
            if ( type == IncomeOrExpense.Expense ) {
                ColorConsole.Write( $"{messageOutput [ MessageEnum.TransMgnt_LabelTransCategory ]} : ", colorByGroup [ ColorGroup.SystemInstructions ] );
                if ( Transactions.Last().Category is not 0 )
                    ColorConsole.WriteLine( $"{Enum.GetName<TransactionCategory>( Transactions.Last().Category )}" );
            }

            if ( saveOnEveryTransaction == true ) {
                WriteTransactionsAndBudget( BudgetOrTransaction.Transaction );
            }
            AnyKeyToContinue();
            return;
        }
        #endregion

        #region >>> ///Report and summary menu and methods
        /// <summary>
        /// Displays a menu that allows the user to generate reports and summaries based on logged transactions.
        /// </summary>
        /// <remarks>If no transactions have been logged, an error message is displayed and the menu is
        /// not shown. The user can navigate the menu using number keys to select report types, or press Escape or
        /// Backspace to exit.</remarks>
        /// <param name="dontClearOnDraw">Indicates whether the console should be cleared before redrawing the menu. The default value is <see
        /// langword="false"/>.</param>
        static void ReportAndSummaryMenu() {
            Console.Write( "\x1b[3J" ); Console.Clear();
            if ( Transactions.Count == 0 ) {

                ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.System_NoReleventTransactions ]} ", colorByGroup [ ColorGroup.SystemError ] );
                AnyKeyToContinue( true );
                return;
            }
            while ( true ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                ReportAndSummaryMenuChoice();
                switch ( Console.ReadKey( intercept: true ).Key ) {
                    //Add Income
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ReportAndSummaryView( SummaryType.All );
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ReportAndSummaryView( SummaryType.Year );
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ReportAndSummaryView( SummaryType.Month );
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        XmlAccountSummary();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Backspace:
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        return;
                }
            }
        }
        /// <summary>
        /// Displays the report and summary menu options to the user, allowing them to choose between different report
        /// types.
        /// </summary>
        /// <remarks>This method outputs a formatted menu to the console, including options for account
        /// overview, yearly summary, monthly summary, and saving summaries as an Excel document. It also provides
        /// instructions for aborting the operation.</remarks>
        static void ReportAndSummaryMenuChoice() {

            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.MainMenu_ReportsAndSummary ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", MenuHeadings );
            ColorConsole.WriteLine( $"\t1. {messageOutput [ MessageEnum.ReportAndSum_AccountOverview ]}", MenuItemColor, ResetColorAfter: false );
            ColorConsole.WriteLine( $"\t2. {messageOutput [ MessageEnum.ReportAndSum_YearlySummary ]}" );
            ColorConsole.WriteLine( $"\t3. {messageOutput [ MessageEnum.ReportAndSum_MonthlySummary ]}" );
            ColorConsole.WriteLine( $"\t4. {messageOutput [ MessageEnum.ReportAndSum_SaveExcel ]}" );
            ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
            ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} 1, 2, 3, {messageOutput [ MessageEnum.Label_Or ]} 4", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
        }

        /// <summary>
        /// Displays a report and summary of transactions for the specified period, including total income, expenses,
        /// balance, and highest expense categories.
        /// </summary>
        /// <remarks>The method prompts the user for a year and, if applicable, a month when displaying
        /// monthly or yearly summaries. If no transactions are found for the selected criteria, an appropriate message
        /// is displayed and the operation is aborted. The summary includes a breakdown of income, expenses, balance,
        /// and the highest expense categories for the selected period.</remarks>
        /// <param name="type">Specifies the type of summary to display. The default value is "SummaryType.All" to show unfiltered transactions.</param>
        static void ReportAndSummaryView( SummaryType type = SummaryType.All ) {
            Console.Write( "\x1b[3J" ); Console.Clear();
            string typeName = "";
            //sets the typeName of the Summary type with a switch based on all, monthly or yearly using messageOutput [ ... ];
            switch ( type ) {
                case SummaryType.All:
                    typeName = messageOutput [ MessageEnum.Label_All ];
                    break;
                case SummaryType.Month:

                    typeName = messageOutput [ MessageEnum.Label_Monthly ];
                    break;
                case SummaryType.Year:
                    typeName = messageOutput [ MessageEnum.Label_Yearly ];
                    break;

            }
            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.MainMenu_ReportsAndSummary ]} ({typeName}) {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.MenuHeadings ] );
            List<Transaction> passedTransactions = new();
            int yearInput = 0;
            int monthInput = 0;
            switch ( type ) {
                case SummaryType.All:
                    passedTransactions = Transactions;
                    break;
                case SummaryType.Year:
                    yearInput = GetYear();
                    if ( yearInput == -1 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_SummaryAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ThreadSleepAndClearKeys( 500, clearScreen: true );
                        return;
                    }
                    //Filter transactions list by LINQ where with yearInput
                    passedTransactions = Transactions
                    .Where( trans =>
                            trans.Date.Year == yearInput ).ToList();
                    if ( passedTransactions.Count == 0 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.ReportAndSum_NoTRansactionsInYear ]} ({yearInput}) ", colorByGroup [ ColorGroup.SystemError ] );
                        AnyKeyToContinue( true );
                        return;
                    }
                    break;
                case SummaryType.Month:
                    yearInput = GetYear();
                    if ( yearInput == -1 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_SummaryAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ThreadSleepAndClearKeys( 500, clearScreen: true );
                        return;
                    }
                    passedTransactions = Transactions.Where( trans => trans.Date.Year == yearInput ).ToList();
                    if ( passedTransactions.Count == 0 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.ReportAndSum_NoTRansactionsInYear ]} ({yearInput}) ", colorByGroup [ ColorGroup.SystemError ] );
                        AnyKeyToContinue( true );
                        return;
                    }
                    //user aborded above
                    if ( yearInput != -1 ) {
                        monthInput = GetMonth();
                    }

                    if ( monthInput == -1 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_SummaryAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ThreadSleepAndClearKeys( 500, clearScreen: true );
                        return;
                    }
                    //Filter transactions list by LINQ where with yearInput
                    passedTransactions = passedTransactions
                    .Where( trans => trans.Date.Month == monthInput ).ToList();
                    if ( passedTransactions.Count == 0 ) {
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.ReportAndSum_NoTRansactionsInMonth ]} ({monthInput}/{yearInput}) ", colorByGroup [ ColorGroup.SystemError ] );
                        AnyKeyToContinue( true );
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_TransactionAborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ThreadSleepAndClearKeys( 500, clearScreen: true );
                        return;
                    }
                    break;
            }

            Console.WriteLine();
            //Collect total expenses from catagories
            Dictionary<TransactionCategory, decimal> TransCatTotals = GetTransactionCategoryTotals( passedTransactions );
            //collect total income, expense and get the balance
            DateOnly earliestDate = Transactions.Min( t => t.Date );
            decimal totalIncome = GetTotalIncome( passedTransactions );
            decimal totalExpense = GetTotalExpense( passedTransactions );
            decimal currentBalance = totalIncome - totalExpense;
            //This improved formating by giving some xtra space and saving cursor position
            ColorConsole.Write( $"{messageOutput [ MessageEnum.ReportAndSum_AccountSummaryFrom ]} ", colorByGroup [ ColorGroup.InputStyleText ] );
            int cursorLeft = Console.CursorLeft;
            switch ( type ) {
                case SummaryType.All:
                    //The date is hardcoded here, may instead want to refrence outputMessages and get the date from there. (Length could be checked by adding 2 numbers at the end and using substr.
                    ColorConsole.WriteLine( $"{earliestDate.ToString( dateFormatDictionary [ DateFormatEnum.NumberMonth ].Item1 )} {messageOutput [ MessageEnum.Label_To ]} {DateOnly.FromDateTime( DateTime.Today ).ToString( "dd/MMM/yyyy" )}", colorByGroup [ ColorGroup.InputStyleText ] );
                    break;
                case SummaryType.Year:
                    ColorConsole.WriteLine( $"01/01/{yearInput} {messageOutput [ MessageEnum.Label_To ]} 31/12/{yearInput}", colorByGroup [ ColorGroup.InputStyleText ] );
                    break;
                case SummaryType.Month:
                    int maxDays = DateTime.DaysInMonth( yearInput, monthInput );
                    ColorConsole.WriteLine( $"01/{monthInput}/{yearInput} {messageOutput [ MessageEnum.Label_To ]} {maxDays}/{monthInput}/{yearInput}", colorByGroup [ ColorGroup.InputStyleText ] );
                    break;
            }
            //Print the summary of added transaction
            Console.WriteLine( "" );
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_TotalIncome ]}", MenuItemColor );
            Console.Write( $" :" );
            Console.CursorLeft = cursorLeft;
            ColorConsole.WriteLine( $"{totalIncome.ToString( "C" )}", ConsoleColor.Green ); // beter practice is to use green when its for money and wont be changed for example, or coul duse menuItemColor if you are ok with that.
            Console.WriteLine( "" );
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_TotalExpenses ]}", MenuItemColor );
            Console.Write( $" :" );
            Console.CursorLeft = cursorLeft;
            ColorConsole.WriteLine( $"({totalExpense.ToString( "C" )})", ConsoleColor.Red );
            Console.WriteLine( "" );
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_TotalBalances ]}", MenuItemColor );
            Console.Write( $" :" );
            Console.CursorLeft = cursorLeft;
            // Formatting as curency adds ( Around the amount ), but in the above exaple we never converted to a negative so I just added brackets instead.
            // could just multiply by -1. (above for expenses)
            if ( currentBalance < 0 )
                ColorConsole.WriteLine( $"{currentBalance.ToString( "C" )}", ConsoleColor.Red );
            else
                ColorConsole.WriteLine( $"{currentBalance.ToString( "C" )}", ConsoleColor.Green );
            Console.WriteLine( "" );
            ColorConsole.Write( $"{messageOutput [ MessageEnum.Label_HighestExpenseCategory ]}", MenuItemColor );
            Console.Write( $" : " );
            for ( int i = 0; i < TransCatTotals.Count; i++ ) {
                ColorConsole.Write( messageOutput [ TransactionCategoryToLanguage [ TransCatTotals.ElementAt( i ).Key ] ], colorByGroup [ ColorGroup.MenuHeadings ] );
                if ( i < TransCatTotals.Count - 1 )
                    Console.Write( ", " );
            }
            ColorConsole.WriteLine( $" : ({TransCatTotals.Last().Value.ToString( "C" )})", ConsoleColor.Red );
            Console.WriteLine();
            AnyKeyToContinue( true );
            return;
        }

        /// <summary>
        /// Generates an Excel account summary report that includes total income, expenses, balance, and recommendations
        /// based on the user's financial data. The report also provides monthly transaction summaries for the past year
        /// and saves the workbook to a file.
        /// </summary>
        /// <remarks>This method creates a new worksheet titled 'Account Summary' and populates it with
        /// formatted financial data. It calculates totals for income and expenses, determines the current balance, and
        /// offers tailored recommendations based on the balance. For each of the past twelve months, it generates a
        /// worksheet summarizing transactions, or notes if no transactions are available. The method handles file
        /// saving and ensures the workbook is not open in another program before saving. Use this method to produce a
        /// comprehensive financial overview in Excel format for user review or record-keeping.</remarks>
        static void XmlAccountSummary() {
            Console.Write( "\x1b[3J" ); Console.Clear();
            List<IXLWorksheet> Worksheets = new();
            using ( XLWorkbook workbook = new() ) {
                Worksheets.Add( workbook.AddWorksheet( messageOutput [ MessageEnum.ReportAndSum_AcountSummary ] ) );
                if ( !workbook.TryGetWorksheet( messageOutput [ MessageEnum.ReportAndSum_AcountSummary ], out var ws ) ) {
                    Console.WriteLine( messageOutput [ MessageEnum.Excel_WorksheetNotFound ] );
                    return;
                }
                const string ExcelAccountingFormat = "_([$$-en-US]* #,##0.00_);_([$$-en-US]* (#,##0.00);_([$$-en-US]* \"-\"??_);_(@_)";
                ws.Cell( "C1" ).Value = messageOutput [ MessageEnum.ReportAndSum_AcountSummary ];
                // Set style through property
                ws.Cell( "C1" ).Style
                    .Font.SetFontSize( 20 )
                    .Font.SetFontName( "Congenial UltraLight" )
                    .Fill.SetBackgroundColor( XLColor.WhiteSmoke );
                int ShiftUnderRulerDwonBy = 0;
                Dictionary<TransactionCategory, decimal> TransCatTotals = GetTransactionCategoryTotals();
                Dictionary<TransactionCategory, IXLRange> HighestCatsRanges = new();
                for ( int i = 0; i < TransCatTotals.Count; i++ ) {
                    HighestCatsRanges.Add( TransCatTotals.ElementAt( i ).Key, ws.Range( $"F{11 + i}:I{11 + i}" ) );
                    IXLRange item = HighestCatsRanges [ TransCatTotals.ElementAt( i ).Key ];
                    item.Merge();
                    item.Style.Font.FontColor = XLColor.Red;
                    item.Value = messageOutput [ TransactionCategoryToLanguage [ TransCatTotals.ElementAt( i ).Key ] ];
                    item.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Center );
                    ShiftUnderRulerDwonBy = i;
                }
                //Create the ranges and add them to a list for looping
                ShiftUnderRulerDwonBy = 3;
                List<IXLRange> Ranges = new();
                var xmlHeader = ws.Range( "C1:H3" );
                var xmlGreeting = ws.Range( "A5:J7" );
                var xmlIncome = ws.Range( "A9:B9" );
                var xmlIncomeArea = ws.Range( "C9:D9" );
                var xmlExpenses = ws.Range( "A11:B11" );
                var xmlExpensesArea = ws.Range( "C11:D11" );
                var xmlBalance = ws.Range( "A13:B13" );
                var xmlBalanceArea = ws.Range( "C13:D13" );
                var xmlHighestCategoryHeader = ws.Range( "F9:I9" );
                var xmlHorizontalRule1 = ws.Range( $"A{12 + ShiftUnderRulerDwonBy}:J{12 + ShiftUnderRulerDwonBy}" );
                var xmlAccountRecomendationHeading = ws.Range( $"D{14 + ShiftUnderRulerDwonBy}:G{14 + ShiftUnderRulerDwonBy}" );
                var xmlAccountRecomendationBody = ws.Range( $"B{16 + ShiftUnderRulerDwonBy}:I{18 + ShiftUnderRulerDwonBy}" );
                Ranges.Add( xmlHeader );
                Ranges.Add( xmlGreeting );
                Ranges.Add( xmlIncome );
                Ranges.Add( xmlIncomeArea );
                Ranges.Add( xmlExpenses );
                Ranges.Add( xmlExpensesArea );
                Ranges.Add( xmlBalance );
                Ranges.Add( xmlBalanceArea );
                Ranges.Add( xmlHighestCategoryHeader );
                Ranges.Add( xmlHorizontalRule1 );
                Ranges.Add( xmlAccountRecomendationHeading );
                Ranges.Add( xmlAccountRecomendationBody );
                //Loop through to merge all of these ranges
                foreach ( var Item in Ranges ) {
                    Item.Merge();
                }
                //Get the total sums
                decimal totalIncome = GetTotalIncome();
                decimal totalExpense = GetTotalExpense();
                decimal currentBalance = totalIncome - totalExpense;

                //Apply Styling
                xmlGreeting.Value = messageOutput [ MessageEnum.Excel_WelcomeMessage ];
                xmlGreeting.Cells().Style.Alignment.SetWrapText( true );

                xmlHeader.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                xmlIncome.Value = $"{messageOutput [ MessageEnum.Label_Income ]}: ";
                xmlIncome.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Right );
                xmlIncome.Style.Font.Bold = true;

                xmlIncomeArea.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                xmlIncomeArea.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                xmlIncomeArea.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                xmlIncomeArea.Value = totalIncome;
                xmlIncomeArea.Style.NumberFormat.SetFormat( ExcelAccountingFormat );

                xmlExpenses.Value = $"{messageOutput [ MessageEnum.Label_Expense ]}: ";
                xmlExpenses.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Right );
                xmlExpenses.Style.Font.Bold = true;

                xmlExpensesArea.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                xmlExpensesArea.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                xmlExpensesArea.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                xmlExpensesArea.Value = totalExpense;
                xmlExpensesArea.Style.NumberFormat.SetFormat( ExcelAccountingFormat );

                xmlBalance.Value = $"{messageOutput [ MessageEnum.Label_Balance ]}: ";
                xmlBalance.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Right );
                xmlBalance.Style.Font.Bold = true;

                xmlBalanceArea.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                xmlBalanceArea.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                xmlBalanceArea.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                xmlBalanceArea.Value = currentBalance;
                xmlBalanceArea.Style.NumberFormat.SetFormat( ExcelAccountingFormat );

                xmlHighestCategoryHeader.Value = messageOutput [ MessageEnum.Label_HighestExpenseCategory ];
                xmlHighestCategoryHeader.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Center );
                xmlHighestCategoryHeader.Style.Font.Bold = true;

                xmlHorizontalRule1.Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                xmlAccountRecomendationHeading.Value = messageOutput [ MessageEnum.Excel_BankRecommendations ];
                xmlAccountRecomendationHeading.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                xmlAccountRecomendationHeading.Style.Fill.BackgroundColor = XLColor.Yellow;
                xmlAccountRecomendationHeading.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Center );
                xmlAccountRecomendationHeading.Style.Alignment.SetVertical( XLAlignmentVerticalValues.Center );

                string reccomendation = "";
                if ( currentBalance > 10000 )
                    reccomendation = messageOutput [ MessageEnum.Excel_BankRec1 ];
                if ( currentBalance > 5000 )
                    reccomendation = messageOutput [ MessageEnum.Excel_BankRec2 ];
                if ( currentBalance > 1000 )
                    reccomendation = messageOutput [ MessageEnum.Excel_BankRec3 ];
                if ( currentBalance > -1000 )
                    reccomendation = messageOutput [ MessageEnum.Excel_BankRec4 ];
                if ( currentBalance > -5000 )
                    reccomendation = messageOutput [ MessageEnum.Excel_BankRec5 ];
                reccomendation = currentBalance > -10000
                    ? messageOutput [ MessageEnum.Excel_BankRec6 ]
                    : messageOutput [ MessageEnum.Excel_BankRec7 ];

                xmlAccountRecomendationBody.Value = reccomendation;
                xmlAccountRecomendationBody.Cells().Style.Alignment.SetWrapText( true );
                xmlAccountRecomendationBody.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Left );
                xmlAccountRecomendationBody.Style.Alignment.SetVertical( XLAlignmentVerticalValues.Top );

                xmlHeader.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Center );
                xmlHeader.Style.Alignment.SetVertical( XLAlignmentVerticalValues.Center );

                DateOnly dateRange = DateOnly.FromDateTime( DateTime.Today );
                // Itterates through each month and creates a new worksheet
                for ( int i = 1; i < 12; i++ ) {
                    List<Transaction> orderedTrans = ( from trans in Transactions
                                                       where trans.Date.Year == dateRange.Year
                                                       && trans.Date.Month == dateRange.Month
                                                       orderby trans.Date descending
                                                       select trans ).ToList();
                    IXLWorksheet sumSheeti = workbook.AddWorksheet( $"{dateRange.Month}, {dateRange.Year}" );
                    if ( orderedTrans.Count > 0 ) {
                        //Set the column widths
                        sumSheeti.Column( 1 ).Width = 10.36;
                        sumSheeti.Column( 2 ).Width = 17;
                        sumSheeti.Column( 3 ).Width = 43.36;
                        sumSheeti.Column( 4 ).Width = 15.55;
                        sumSheeti.Row( 1 ).Style.Font.Bold = true;
                        //Give handles to the header rows for turning into a table later
                        var date = sumSheeti.Cell( 1, 1 ).Value = messageOutput [ MessageEnum.Label_Date ];
                        var transCat = sumSheeti.Cell( 1, 2 );
                        var description = sumSheeti.Cell( 1, 3 );
                        var amount = sumSheeti.Cell( 1, 4 );
                        transCat.Value = messageOutput [ MessageEnum.Label_Category ];
                        description.Value = messageOutput [ MessageEnum.Label_Description ];
                        amount.Value = messageOutput [ MessageEnum.Label_Amount ];
                        int index = 2;
                        //Write each transaction
                        foreach ( Transaction trans in orderedTrans ) {
                            var transDateCells = sumSheeti.Cell( index, 1 );
                            transDateCells.Value = trans.Date.ToString();
                            transDateCells.Style.NumberFormat.Format = dateFormatDictionary [ DateFormatEnum.NumberMonth ].Item1;
                            var transCatCells = sumSheeti.Cell( index, 2 );
                            transCatCells.Value = messageOutput [ TransactionCategoryToLanguage [ trans.Category ] ];
                            var transDescCells = sumSheeti.Cell( index, 3 );
                            transDescCells.Value = trans.Description;
                            var transAmountCells = sumSheeti.Cell( index, 4 );
                            transAmountCells.Value = trans.Category != TransactionCategory.Income ? ( XLCellValue ) ( trans.Amount * -1 ) : ( XLCellValue ) trans.Amount;
                            transAmountCells.Style.NumberFormat.Format = ExcelAccountingFormat;
                            index++;
                        }
                        var lastRow = sumSheeti.LastRowUsed().RowNumber();
                        var lastCol = sumSheeti.LastColumnUsed().ColumnNumber();
                        var range = sumSheeti.Range( 1, 1, lastRow, lastCol );
                        var table = range.CreateTable();
                    } else {
                        var noInfo = sumSheeti.Range( "B3:I4" );
                        noInfo.Merge();
                        noInfo.Value = messageOutput [ MessageEnum.Excel_EmptyMonth ];
                        noInfo.Style.Alignment.SetHorizontal( XLAlignmentHorizontalValues.Center );
                        noInfo.Style.Alignment.SetVertical( XLAlignmentVerticalValues.Center );
                        noInfo.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                        noInfo.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                        noInfo.Cells().Style.Alignment.SetWrapText( true );
                    }
                    dateRange = dateRange.AddMonths( -1 );
                }
                int fileI = 0;
                string suf = "";
                while ( true ) {
                    try {
                        string FileName = $"account_summary{suf}.xlsx";
                        workbook.SaveAs( FileName );
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Excel_SavedMessage ]} {messageOutput [ MessageEnum.Label_FileName ]} : {FileName}", colorByGroup [ ColorGroup.MenuItems ] );
                        //Conventionf or using colors directly. allows color change. 
                        Console.ForegroundColor = colorByGroup [ ColorGroup.SystemInstructionsGray ] [ 0 ];
                        Console.BackgroundColor = colorByGroup [ ColorGroup.SystemInstructionsGray ] [ 1 ];
                        AnyKeyToContinue( true );
                    } catch ( IOException ) {
                        fileI++;
                        if ( fileI < 20 ) {
                            suf = fileI.ToString();
                            continue;
                        } else {
                            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Excel_FileNoAccessMessage ]} ", colorByGroup [ ColorGroup.SystemWarning ] );
                            AnyKeyToContinue();
                        }
                    }
                    break;
                }
            }
        }
        #endregion

        #region >>> ///Options Menu
        /// <summary>
        /// Displays the options menu, allowing the user to interact with various application settings and actions.
        /// </summary>
        /// <remarks>The menu responds to specific key presses to perform actions such as adding income,
        /// toggling the save-on-transaction setting, or exiting the menu. Pressing Escape or Backspace will close the menu
        /// and return to the previous screen.</remarks>
        static void OptionsMenu() {

            while ( true ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                OptionsMenuChoice();
                switch ( Console.ReadKey( intercept: true ).Key ) {
                    //Add Income
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ChooseLanguage( bypassConfig: true );
                        break;
                    //Change auto save
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        saveOnEveryTransaction = saveOnEveryTransaction == false;
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        break;
                    // Load sample data and overwrite transactions
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ConsoleColor test = Console.ForegroundColor;
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        //just hardcoded because this is a dev tool only, language options not neeeded. stays english on purpose.
                        ColorConsole.WriteLine( "(Temporary Dev Tool) Loading sample data will delete your current transactions and editing/saving will overwrite the file.", colorByGroup [ ColorGroup.SystemWarning ] );
                        ColorConsole.Write( $"\n{messageOutput [ MessageEnum.Label_Press ]} : ({messageOutput [ MessageEnum.SystemInstructions_SpaceOrEnter ]})", colorByGroup [ ColorGroup.MenuItems ] );
                        ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Or ].ToUpper()} ", ConsoleColor.White );
                        ColorConsole.WriteLine( $"({messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                        while ( true ) {
                            ConsoleKey key = Console.ReadKey( intercept: true ).Key;
                            if ( key == ConsoleKey.Spacebar || key == ConsoleKey.Enter ) {
                                //Delete transactions
                                Transactions.Clear();
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                SampleTransactionData();
                                AnyKeyToContinue();
                                break;
                            }
                            if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_Aborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                                ThreadSleepAndClearKeys( 500, clearScreen: true );
                                break;
                            }
                        }
                        break;
                    // Delete all transactions
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Warning_DeleteTransactions ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                        ColorConsole.Write( $"\n{messageOutput [ MessageEnum.Label_Press ]} : ({messageOutput [ MessageEnum.SystemInstructions_SpaceOrEnter ]})", colorByGroup [ ColorGroup.MenuItems ] );
                        ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Or ].ToUpper()} ", ConsoleColor.White );
                        ColorConsole.WriteLine( $"({messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                        while ( true ) {
                            ConsoleKey key = Console.ReadKey( intercept: true ).Key;
                            if ( key == ConsoleKey.Spacebar || key == ConsoleKey.Enter ) {
                                //Delete transactions
                                Transactions.Clear();
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.DataOptions_TransactionsDeleted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                                AnyKeyToContinue();
                                break;
                            }
                            if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.Label_Aborted ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                                ThreadSleepAndClearKeys( 500, clearScreen: true );
                                break;
                            }
                        }
                        break;
                    //Print transaction count in memory
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        Console.WriteLine( $"{messageOutput [ MessageEnum.DataOptions_LabelAmountOfTrans ]} : {Transactions.Count}" );
                        Console.WriteLine( $"({messageOutput [ MessageEnum.DataOptions_WarningThisPrintsOnlyRam ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                        AnyKeyToContinue();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Backspace:
                        return;
                }

            }
        }

        /// <summary>
        /// Displays the options menu for user configuration, allowing the selection of language and file save
        /// preferences.
        /// </summary>
        /// <remarks>This method presents a menu to the user, providing options to change the application
        /// language and to enable or disable automatic file saving after each change. The current state of the save
        /// option is indicated in the menu. User instructions for making a selection are also displayed.</remarks>
        static void OptionsMenuChoice() {
            ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.MainMenu_Options ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", MenuHeadings );
            ColorConsole.WriteLine( $"\t1. {messageOutput [ MessageEnum.Options_ChangLang ]}", MenuItemColor, ResetColorAfter: false );
            ColorConsole.Write( $"\t2. {messageOutput [ MessageEnum.Options_AutoSave ]} : " );
            Console.WriteLine( saveOnEveryTransaction ? messageOutput [ MessageEnum.Label_Yes ] : messageOutput [ MessageEnum.Label_No ] );
            ColorConsole.WriteLine( $"\t3. {messageOutput [ MessageEnum.DataOptions_LoadSample ]}" );
            ColorConsole.WriteLine( $"\t4. {messageOutput [ MessageEnum.DataOptions_DeleteTransactions ]}" );
            ColorConsole.WriteLine( $"\t5. {messageOutput [ MessageEnum.DataOptions_PrintTransactionCount ]}" );
            ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
            ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Press ]} 1, 2, 3, 4, {messageOutput [ MessageEnum.Label_Or ]} 5", colorByGroup [ ColorGroup.Default ], ResetColorAfter: false );
        }
        #endregion

        #region >>> ///GetData
        /// <summary>
        /// Prompts the user to enter and confirm a secure password for accessing transaction database files. Enforces
        /// password security requirements to help protect sensitive data.
        /// Cooldown implemented to prevent brute force attacks, and clear instructions provided to the user about password
        /// requirements and the importance of remembering the password.
        /// </summary>
        /// <remarks>The method enforces password security requirements, including a minimum length of 15
        /// characters, the inclusion of digits, special characters, and mixed case letters. If the passwords do not
        /// match during confirmation, the user is prompted to re-enter the password.</remarks>
        /// <param name="noFile">A boolean value indicating whether to prompt for a new password (<see langword="true"/>) or to confirm an
        /// existing password (<see langword="false"/>).</param>
        /// <returns>The securely entered password as a string, which is required for accessing the transaction database.</returns>
        static string GetPwd( bool noFile ) {
            string pwdFirst = "";
            string pwdInput;
            bool first_input = true;
            Regex pattern = new( "^(?=.*([!-/:-@\\[-`{-~]))(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\s]).{15,}$" );
            int i = 0;
            while ( true ) {
                i++;
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.GetPwd_Header ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}\n", colorByGroup [ ColorGroup.MenuHeadings ] );
                ColorConsole.WriteLine( messageOutput [ MessageEnum.GetPwd_PwSafteyReminder ], colorByGroup [ ColorGroup.SystemWarning ] );
                Console.WriteLine();
                ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.GetPwd_SecurePwIsHeader ]} :", colorByGroup [ ColorGroup.MenuItems ] );
                Console.WriteLine();
                ColorConsole.WriteLine( messageOutput [ MessageEnum.GetPwd_Instruction15Chars ] +
               $"\n\t• {messageOutput [ MessageEnum.GetPwd_InstructionContainDigit ]} [0-9]," +
               $"\n\t• {messageOutput [ MessageEnum.GetPwd_InstructionSpecialChar ]}, [!@#$%^&*()_+-={{}}[]|\\;:'\",<.>/?`~]" +
               $"\n\t• {messageOutput [ MessageEnum.GetPwd_InstructionMixCase ]}" );
                if ( pwdFirst == "" )
                    ColorConsole.Write( $"\n{messageOutput [ MessageEnum.GetPwd_EnterPw ]} : ", colorByGroup [ ColorGroup.Header ] );
                else
                    ColorConsole.Write( $"\n{messageOutput [ MessageEnum.GetPwd_ConfirmPw ]} : ", colorByGroup [ ColorGroup.Header ] );
                Console.CursorVisible = true;
                pwdInput = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] )!;
                Console.Write( "\x1b[3J" ); Console.Clear();
                Console.CursorVisible = false;
                //Password Matches
                if ( pattern.IsMatch( pwdInput ) ) {
                    if ( first_input ) {
                        pwdFirst = pwdInput;
                        first_input = false;
                        continue;
                    }
                    //Password Confirm attempt
                    else {
                        //Return password
                        if ( pwdInput == pwdFirst )
                            return pwdFirst;
                        //The two passwords do not match restart process
                        else {
                            ColorConsole.WriteLine( messageOutput [ MessageEnum.GetPwd_Warning_PwDontMatch ], colorByGroup [ ColorGroup.SystemWarning ] );
                            pwdFirst = "";
                            first_input = true;
                        }
                    }
                }
                //Password Not Secure Ebough
                else ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.GetPwd_Warning_PwDontMeetCriteria ]} ", colorByGroup [ ColorGroup.SystemError ] );
            }
        }
        /// <summary>
        /// Get an amount from the user by typing it in and validate
        /// </summary>
        /// <param name="amt">Variable to update with the ammount</param>
        /// <param name="allowInputZero">Specify weather the user can input a 0 or not. Good for income and expenses</param>
        /// <returns></returns>
        static bool GetAmount( ref decimal amt, bool allowInputZero = false ) {
            //Ask enter amount
            while ( true ) {
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_Abort ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.SystemInstructions_InputIncomeAmount ]} : ", colorByGroup [ ColorGroup.SystemInstructions ] );
                string incomeInput = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] );
                // user typed exit in native language, returning
                if ( incomeInput == messageOutput [ MessageEnum.Label_Exit ] ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    return false;
                }
                if ( !decimal.TryParse( incomeInput, out amt ) || ( allowInputZero ? amt < 0 : amt <= 0 ) ) {
                    ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]}) ", colorByGroup [ ColorGroup.SystemWarning ] );
                    if ( allowInputZero )
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_BadAmountZeroOk ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    else
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_BadAmountNoZero ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    continue;
                } else {
                    //Prevent absurd transactions sizes, suite to taste. important for window size and formatting
                    if ( amt > 999999999999.9m ) {
                        ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]})", colorByGroup [ ColorGroup.SystemInstructions ] );
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.GetPwd_Warning_TooManyAttempts ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Extends the Add Income Expense method, where if the user is adding expense it will ask for the specific category.
        /// Uses readKey to set the category via ref.
        /// </summary>
        /// <param name="cat">Pass refrence to the category lsit the user is checking</param>
        /// <returns>False if the user aborted</returns>
        static bool GetCategory( ref TransactionCategory cat, bool usedForBudgetMethod = false, bool viewAllCatergories = false ) {
            int menuPadding = 8;
            //Ask category if Type expense
            Console.WriteLine();
            ColorConsole.WriteLine( messageOutput [ MessageEnum.GetCategory_ChooseCategory ], colorByGroup [ ColorGroup.InputStyleText ] );
            Console.WriteLine();
            TransactionCatagoriesOrderedList( padding: menuPadding, usedForBudgetMethod: usedForBudgetMethod, viewByCatagory: viewAllCatergories );
            Console.WriteLine();
            ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_PressToExit ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.GetCategory_InstructionHowMakeChoice ]}", colorByGroup [ ColorGroup.SystemInstructions ] );
            //Process users choice
            int incomeOffsetAtZero = 0;
            ConsoleKey lastKey = ConsoleKey.C;
            if ( viewAllCatergories ) {
                incomeOffsetAtZero = 1;
                lastKey = ConsoleKey.D;
            }
            while ( true ) {
                ConsoleKeyInfo keyInfo = Console.ReadKey( intercept: true );
                //Sets the Key offset for 3 key ranges. Assumes fixed length enum for now. Change ConsoleKey.D if adding 
                //transaction catagories or expecting the user to be able to add transaction catagories.
                if ( keyInfo.Key > ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9 ) {
                    cat = ( TransactionCategory ) ( keyInfo.Key - 48 - incomeOffsetAtZero ); //Compensates for 0 index income
                    break;
                }
                if ( keyInfo.Key > ConsoleKey.NumPad0 && keyInfo.Key <= ConsoleKey.NumPad9 ) {
                    cat = ( TransactionCategory ) ( keyInfo.Key - 97 - incomeOffsetAtZero );
                    break;
                }
                if ( keyInfo.Key >= ConsoleKey.A && keyInfo.Key < lastKey + 1 ) {
                    cat = ( TransactionCategory ) ( keyInfo.Key - 64 + 9 - incomeOffsetAtZero ); //Plus 9 is because this starts at A
                    break;
                }
                // user typed exit in native language, returning
                if ( keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Backspace ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get the longest length of the transaction types. 
        /// Used for formatting to keep everything lined up with the longest category name.
        /// This is mainly to prevent issues if the Transaction enum is updated. Although full code doesnt accomodate larger lists
        /// such as the menu for selecting categories which is currently hard coded to income plus 12 more categories, this at least allows for some
        /// formatting flexibility if the enum is updated without updating the formatting code.
        /// </summary>
        /// <returns>Returns longest Enum Transaction Category length Ie Gas, Insurance, Food, this would return 9 for insurance.</returns>
        static int GetTransactionCategoryLongestLength() {
            foreach ( TransactionCategory cat in Enum.GetValues( typeof( TransactionCategory ) ) ) {
                int catLen = messageOutput [ TransactionCategoryToLanguage [ cat ] ].Length;
                if ( transactionCategoryLongestSize < catLen )
                    transactionCategoryLongestSize = catLen;
            }
            return transactionCategoryLongestSize;
        }

        /// <summary>
        /// Grabs category totals of the largest categories! Ideally 1 is returned unless more then one "largest" (i.e. same highest value)
        /// </summary>
        /// <returns>Returns a dictionary item of the TransactionCategory and decimal.</returns>
        static Dictionary<TransactionCategory, decimal> GetTransactionCategoryTotals( List<Transaction>? passedTransactions = null ) {
            //Provide total expenses of all transactions if a filtered list is not provided
            if ( passedTransactions is null )
                passedTransactions = Transactions;
            Dictionary<TransactionCategory, decimal> TransCatTotals = new();
            //Loop through each Category Type
            decimal highestTotal = 0;
            foreach ( TransactionCategory cat in Enum.GetValues( typeof( TransactionCategory ) ) ) {
                //Skip income category
                if ( cat != 0 ) {
                    decimal totalCategorySum = ( from trans in passedTransactions
                                                 where trans.Category == cat
                                                 select trans ).Sum( trans => trans.Amount );
                    if ( totalCategorySum > highestTotal ) { //If greater then drop whole list
                        TransCatTotals.Clear();
                        TransCatTotals.Add( cat, totalCategorySum );
                        highestTotal = totalCategorySum;
                    } else  //if equal add to the list
                        if ( totalCategorySum == highestTotal )
                            TransCatTotals.Add( cat, totalCategorySum );
                }
            }
            return TransCatTotals;
        }

        /// <summary>
        /// Sums up the total income of all income transactions labeled with  TransactionCategory Income
        /// THIS CAN BE EXTENDED by passing it a list that has been ordered, as of 2026 march 1st this is not used
        /// </summary>
        /// <param name="passedTransactions">Pass a transaction list that is filtered, such as by date range or amounts.</param>
        /// <returns>Total income of all transactions.</returns>
        static decimal GetTotalIncome( List<Transaction>? passedTransactions = null ) {
            //Provide total income of all transactions if a filtered list is not provided
            if ( passedTransactions is null )
                passedTransactions = Transactions;
            decimal totalIncome = ( from trans in passedTransactions
                                    where trans.Category == TransactionCategory.Income
                                    select trans ).Sum( trans => trans.Amount );
            return totalIncome;
        }

        /// <summary>
        /// Sums up the total expenses of all expenses transactions labeled with TransactionCategory that is not Income
        /// THIS CAN BE EXTENDED by passing it a list that has been ordered, as of 2026 march 1st this is not used
        /// </summary>
        /// <param name="passedTransactions">Pass a transaction list that is filtered, such as by date range or amounts.</param>
        /// <returns>Total expenses of all transactions.</returns>
        static decimal GetTotalExpense( List<Transaction>? passedTransactions = null ) {

            //Provide total expenses of all transactions if a filtered list is not provided
            if ( passedTransactions is null )
                passedTransactions = Transactions;
            decimal totalExpense = ( from trans in passedTransactions
                                     where trans.Category != TransactionCategory.Income
                                     select trans ).Sum( trans => trans.Amount );
            return totalExpense;
        }

        #region >>> ///Get Various Dates (Grouped Class Members - Collapse for readability)
        /// <summary>
        /// Get a date from a user in the formatting specified by DateFormatEnum, with an option to exit.
        /// Uses custom language dictionary for the prompts and messages as well as a custom color method for formatting.
        /// </summary>
        /// <param name="date">Date to update</param>
        static bool GetDate( ref DateOnly date ) {
            while ( true ) {
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_Abort ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.SystemInstructions_EnterDate ]} {messageOutput [ MessageEnum.Warning_DateFormat ]} : ", colorByGroup [ ColorGroup.SystemPromptInstructions ] );
                string DateInput = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] );
                // user typed exit in native language, returning
                if ( DateInput == messageOutput [ MessageEnum.Label_Exit ] ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    return false;
                }
                //uses a tuple to get the format type accociated stirng, storing the space used by MM, MMM, or MMMM (max is 9 'september')
                if ( !DateOnly.TryParseExact( DateInput, dateFormatDictionary [ DateFormatEnum.NumberMonth ].Item1, out date ) ) {
                    ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                    Console.Write( " " );
                    ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_BadDate ]} {messageOutput [ MessageEnum.Warning_DateFormat ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    continue;
                }
                if ( date.Year < DateTime.Today.Year - cutofdate || date > DateOnly.FromDateTime( DateTime.Today ) ) {
                    ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                    Console.Write( " " );
                    if ( date.Year < DateTime.Today.Year - cutofdate )
                        ColorConsole.WriteLine( $" ({messageOutput [ MessageEnum.Warning_BadDate ]}) {messageOutput [ MessageEnum.Warning_InvalidYearOld ]} : {cutofdate} ", colorByGroup [ ColorGroup.SystemError ] );
                    else
                        ColorConsole.WriteLine( $" ({messageOutput [ MessageEnum.Warning_BadDate ]}) {messageOutput [ MessageEnum.Warning_InvalidYearNew ]} ", colorByGroup [ ColorGroup.SystemError ] );

                    continue;
                }
                break;
            }
            return true;
        }
        /// <summary>
        /// GetDate Overloaded method to get two dates and compare them, used for the report summary menu to get a date range.
        /// Get a date from a user in the formatting specified by DateFormatEnum, with an option to exit.
        /// Uses custom language dictionary for the prompts and messages as well as a custom color method for formatting.
        /// </summary>
        /// <param name="date">Date to update</param>
        /// <param name="compareDate">Previous date to compare to</param>
        static bool GetDate( ref DateOnly date, ref DateOnly compareDate ) {
            while ( true ) {
                if ( GetDate( ref date ) )
                    return true;
                if ( date == compareDate ) {
                    Console.WriteLine( messageOutput [ MessageEnum.GetDate_SameDates ] );
                    continue;
                } else if ( date < compareDate ) {
                    //Swamping dates to keep the low ranges in order, may not be needed!
                    DateOnly swapDate = date;
                    date = compareDate;
                    compareDate = swapDate;
                }
                Console.WriteLine( $"{messageOutput [ MessageEnum.GetDate_SearchingDatesBetween ]} {date.ToString( dateFormatOut.Item1 )} {messageOutput [ MessageEnum.Label_And ]} {compareDate.ToString( dateFormatOut.Item1 )}" );
                return false;
            }
        }
        /// <summary>
        /// Specifically gets an integer between 120 years from todays date and todays date, with an option to exit.
        /// Uses custom language dictionary for the prompts and messages as well as a custom color method for formatting.
        /// </summary>
        /// <returns>120 years from preset date to present date</returns>
        static int GetYear() {
            while ( true ) {
                int yearInput = 0;
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_Abort ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.SystemInstructions_InputYearForSummary ]} : ", colorByGroup [ ColorGroup.SystemPromptInstructions ] );
                string yearStr = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] );

                if ( yearStr == messageOutput [ MessageEnum.Label_Exit ] ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    return -1;
                }
                if ( int.TryParse( yearStr, out yearInput ) ) {
                    if ( yearInput > DateTime.Now.Year ) {
                        ColorConsole.WriteLine( $" ({messageOutput [ MessageEnum.Warning_BadDate ]}) {messageOutput [ MessageEnum.Warning_InvalidYearNew ]}", colorByGroup [ ColorGroup.SystemError ] );
                        continue;
                    }
                    if ( yearInput < DateTime.Now.Year - cutofdate ) {
                        ColorConsole.WriteLine( $" ({messageOutput [ MessageEnum.Warning_BadDate ]}) {messageOutput [ MessageEnum.Warning_InvalidYearOld ]} : {cutofdate} ", colorByGroup [ ColorGroup.SystemError ] );
                        continue;
                    }
                    return yearInput;
                } else {
                    ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                    Console.Write( " " );
                    ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_BadDate ]} {messageOutput [ MessageEnum.Warning_DateFormatYYYY ]} ", colorByGroup [ ColorGroup.SystemError ] );
                }
            }
        }
        /// <summary>
        /// Specifically gets an integer between 1 and 12 for the month, with an option to exit.
        /// Uses custom language dictionary for the prompts and messages as well as a custom color method for formatting.
        /// </summary>
        /// <returns>integer from 1 to 12</returns>
        static int GetMonth() {
            while ( true ) {
                int monthInput = 0;
                ColorConsole.Write( $"({messageOutput [ MessageEnum.SystemInstructions_Abort ]})", colorByGroup [ ColorGroup.SystemPromptHint ] );
                ColorConsole.Write( $" {messageOutput [ MessageEnum.SystemInstructions_InputMonthForSummary ]} : ", colorByGroup [ ColorGroup.SystemPromptInstructions ] );
                string monthStr = ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] );
                if ( monthStr == messageOutput [ MessageEnum.Label_Exit ] ) {
                    return -1;
                }
                if ( int.TryParse( monthStr, out monthInput ) ) {
                    if ( monthInput < 1 || monthInput > 12 ) {
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_InvalidMonth ]} ", colorByGroup [ ColorGroup.SystemError ] );
                        continue;
                    }
                    if ( monthInput > DateTime.Now.Month ) {
                        ColorConsole.WriteLine( $" ({messageOutput [ MessageEnum.Warning_BadDate ]}) {messageOutput [ MessageEnum.Warning_InvalidYearNew ]}", colorByGroup [ ColorGroup.SystemError ] );
                        continue;
                    }
                    return monthInput;
                } else {
                    ColorConsole.Write( $"({messageOutput [ MessageEnum.Warning_BadInput ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                    Console.Write( " " );
                    ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_InvalidMonth ]} ", colorByGroup [ ColorGroup.SystemError ] );
                }
            }
        }
        #endregion
        #endregion

        #region >>> ///Load and Write Language, Transactions and Budgets
        /// <summary>
        /// If config file exists it will try to load the language specified in the config file, if it fails it will fall back to the default english dictionary and overwrite the config file with the default english language code.
        /// If the config is laoded it will prompt the user to select a language from the list of languages in the xml file, and save the selected language to the config file for next time. If the user selects a language that is not in the xml file, it will fall back to the default english dictionary and overwrite the config file with the default english language code.
        /// If the config file is bypassed it will prompt the user to select a language from the list of languages in the xml file, and save the selected language to the config file for next time. If the user selects a language that is not in the xml file, it will fall back to the default english dictionary and overwrite the config file with the default english language code.
        /// This by pass is used by the Options menu to allow the user to change their language without having to delete the config file or manually change the language code in the config file.
        /// </summary>
        /// <returns>language is 2 letter stirng</returns>
        static void ChooseLanguage( ConsoleColor [ ]? errorFgAndBg = null, ConsoleColor [ ]? prevFgAndBg = null, ConsoleColor [ ]? headerFgAndBg = null, ConsoleColor [ ]? highlightFgAndBg = null, bool bypassConfig = false ) {
            Console.Write( "\x1b[3J" ); Console.Clear();
            string languageName = "";
            string fallthrough = defaultEnglishMessages [ MessageEnum.ChooseLang_RevertingToEng ];
            List<string>? langsFoundLabelsTry;
            List<string> langsFoundLabels = new();
            Dictionary<string, string> dict;
            Dictionary<string, string> ChooseLanguagePrompt = new();
            bool exceptionCaught = false;
            //important must call this first to load the instructions in various languages.
            defineDefaultInstruction();
            string langKey = string.IsNullOrWhiteSpace( language ) ? CultureInfo.CurrentUICulture.TwoLetterISOLanguageName : language;
            string currentCultureInstruction = ChooseLanguagePrompt.TryGetValue( langKey, out var msg ) ? msg : ChooseLanguagePrompt [ "en" ];
            var xdoc = new XDocument();
            //Load config file with langauge if it exists already
            bool refPW = false; //placeholder, password hardcoded should be fine
            bool loadLanguageOptionsListFromFile = false;
            Configuration? loadedConfig = SecureFile.Load<Configuration>( configFile, ref refPW );
            if ( loadedConfig is not null && !bypassConfig ) {
                config = loadedConfig;
                loadLanguageOptionsListFromFile = true;
            }
            if ( !loadLanguageOptionsListFromFile ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                //Checks the dictionary for current language, if the key doesnt exist it will catch and use defualt english hardcoded
                try {
                    ColorConsole.Write( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.ChooseLang_Header ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} \n", colorByGroup [ ColorGroup.MenuItems ] );
                } catch ( Exception ) {
                    ColorConsole.Write( $"{defaultEnglishMessages [ MessageEnum.Menu_HeaderOuterDecor ]} {defaultEnglishMessages [ MessageEnum.ChooseLang_Header ]} {defaultEnglishMessages [ MessageEnum.Menu_HeaderOuterDecor ]} \n", colorByGroup [ ColorGroup.MenuItems ] );
                }
                ThreadSleepAndClearKeys( 500 );
            }
            try {
                xdoc = XDocument.Parse( langFile ); //Load the language file
                short selectedListNumber;
                //Get langauges in the file
                List<string [ ]> Langs = new();
                langsFoundLabelsTry = xdoc.Root?
                                       .Elements()
                                        .Select( e => e.Name.LocalName ?? string.Empty )
                                        .ToList();
                int i = 0;
                List<string [ ]> langaugeCodeAndName = new();
                //list the langages
                if ( !loadLanguageOptionsListFromFile ) {
                    foreach ( var e in langsFoundLabelsTry ) {
                        ThreadSleepAndClearKeys( 7 );
                        i++;
                        try {
                            langaugeCodeAndName.Add(
                            !string.IsNullOrWhiteSpace( CultureInfo.GetCultureInfo( e ).NativeName )
                                ? new [ ] { e, char.ToUpper( CultureInfo.GetCultureInfo( e ).NativeName.First() ) + CultureInfo.GetCultureInfo( e ).NativeName.Substring( 1 ) }
                                : new [ ] { e, char.ToUpper( CultureInfo.GetCultureInfo( e ).NativeName.First() ) + CultureInfo.GetCultureInfo( e ).EnglishName.Substring( 1 ) } );
                        } catch ( Exception ) {
                            langaugeCodeAndName.Add( new [ ] { e, e } );
                            //tries the current langauge, if there is no key it will use defualt
                            try {
                                Console.WriteLine( $"{messageOutput [ MessageEnum.Warning_CultureNotFound ]} : {e}" );
                            } catch ( Exception ) {
                                Console.WriteLine( $"{defaultEnglishMessages [ MessageEnum.Warning_CultureNotFound ]} : {e}" );
                            }

                        }
                        langsFoundLabels.Add( e );
                        //Highlight the current culture in the list, to make it esier find their language
                        if ( e.Equals( CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Substring( 0, 2 ), StringComparison.OrdinalIgnoreCase ) )
                            ColorConsole.Write( $"\t{i}. {langaugeCodeAndName [ i - 1 ] [ 1 ]}", colorByGroup [ ColorGroup.MenuHeadings ] );
                        else
                            ColorConsole.WriteLine( $"\t{i}. {langaugeCodeAndName [ i - 1 ] [ 1 ]}" );
                    }

                    Console.WriteLine();
                    Console.Write( currentCultureInstruction );
                    //Checks users input compared to list
                }
                if ( !loadLanguageOptionsListFromFile ) {
                    while ( true ) {
                        //Numbers and Number Pad returns 0 - 8 index based on 1 - 9 input
                        if ( short.TryParse( ColorConsole.ReadLine( colorByGroup [ ColorGroup.InputStyleText ] ), out selectedListNumber ) && selectedListNumber < i + 1 && selectedListNumber > 0 )
                            break;
                        //Checks for key if not existing uses defualt
                        try {
                            ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Warning_LanguageNotInList ]} ", colorByGroup [ ColorGroup.SystemError ] );

                        } catch ( Exception ) {
                            ColorConsole.WriteLine( $" {defaultEnglishMessages [ MessageEnum.Warning_LanguageNotInList ]} ", colorByGroup [ ColorGroup.SystemError ] );

                        }
                        Console.Write( currentCultureInstruction );
                    }
                    selectedListNumber--;
                    language = langaugeCodeAndName [ selectedListNumber ] [ 0 ];
                    languageName = langaugeCodeAndName [ selectedListNumber ] [ 1 ];
                } else {
                    language = config.language;
                    languageName = CultureInfo.GetCultureInfo( config.language ).NativeName;
                }
                dict = xdoc?.Root?
                           .Descendants( language )?
                           .Descendants( "item" )?
                           .Where( x => x.Attribute( "key" ) != null )
                           .ToDictionary(
                                x => x.Attribute( "key" )!.Value,
                                x => x.Element( "message" )?.Value ?? string.Empty
                             ) ?? new Dictionary<string, string>();
                messageOutput.Clear();
                foreach ( MessageEnum item in Enum.GetValues<MessageEnum>() ) {
                    if ( dict.ContainsKey( item.ToString() ) ) {
                        messageOutput.Add( item, dict [ item.ToString() ] );
                    } else {
                        if ( defaultEnglishMessages.ContainsKey( item ) ) {
                            messageOutput.Add( item, defaultEnglishMessages [ item ] );
                        } else {
                            messageOutput.Add( item, $"{{{item.ToString()}}}" );
                        }
                    }
                }
                //Add code here to load language
                config.language = language;
                SecureFile.Save( configFile, config );
                //just left these errors as defualt english, assuming issue with xml means its defualting to english anyways
            } catch ( XmlException ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Warning_XmlFormat ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( UnauthorizedAccessException ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Excel_FileNoAccessMessage ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( ArgumentException ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Warning_ArgumentIssue ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( FileNotFoundException ) { ColorConsole.Write( $" {messageOutput [ MessageEnum.Warning_FileNotFound ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( DirectoryNotFoundException ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Warning_DirectoriesNotFound ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( NullReferenceException ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Warning_FileNull ]} ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; } catch ( Exception ) { ColorConsole.Write( $" {defaultEnglishMessages [ MessageEnum.Warning_GeneralException ]}  ", colorByGroup [ ColorGroup.SystemError ] ); exceptionCaught = true; }
            if ( exceptionCaught ) {
                Console.WriteLine( $" {fallthrough}" );
                messageOutput = defaultEnglishMessages;
            }
            ColorConsole.Write( $"{( ( languageName == "" ) ? "English" : languageName )} : {messageOutput [ MessageEnum.ChooseLang_LangApplied ]} ({messageOutput [ MessageEnum.Label_Press ]} : {messageOutput [ MessageEnum.System_AnyKeyToContinue ]}) ", colorByGroup [ ColorGroup.MenuItems ] );
            Console.ReadKey( true );
            Console.Write( "\x1b[3J" ); Console.Clear();
            ///Sets the foreground and background to the supplied colors, or default if not supplied.
            ///Adds a writeLine to ensure there is no color bleeding from any ConsoleWrite before.
            ///Therefore, only use console.Write before this, not WriteLine. This will drop a new line for you.
            void defineDefaultInstruction() {
                ChooseLanguagePrompt.Add( "de", "Wählen Sie eine Sprache, indem Sie die entsprechende Nummer eingeben : " );
                ChooseLanguagePrompt.Add( "it", "Scegli una lingua inserendo il numero corrispondente : " );
                ChooseLanguagePrompt.Add( "pt", "Escolha um idioma digitando o número correspondente : " );
                ChooseLanguagePrompt.Add( "nl", "Kies een taal door het overeenkomstige nummer in te voeren : " );
                ChooseLanguagePrompt.Add( "sv", "Välj ett språk genom att ange motsvarande nummer : " );
                ChooseLanguagePrompt.Add( "no", "Velg et språk ved å skrive inn det tilsvarende nummeret : " );
                ChooseLanguagePrompt.Add( "da", "Vælg et sprog ved at indtaste det tilsvarende nummer : " );
                ChooseLanguagePrompt.Add( "fi", "Valitse kieli syöttämällä vastaava numero : " );
                ChooseLanguagePrompt.Add( "is", "Veldu tungumál með því að slá inn samsvarandi númer : " );
                ChooseLanguagePrompt.Add( "ga", "Roghnaigh teanga trí an uimhir chomhfhreagrach a iontráil : " );
                ChooseLanguagePrompt.Add( "cy", "Dewiswch iaith drwy nodi'r rhif cyfatebol : " );
                ChooseLanguagePrompt.Add( "eu", "Hautatu hizkuntza dagokion zenbakia sartuta : " );
                ChooseLanguagePrompt.Add( "ca", "Tria un idioma introduint el número corresponent : " );
                ChooseLanguagePrompt.Add( "gl", "Escolle un idioma introducindo o número correspondente : " );
                ChooseLanguagePrompt.Add( "ro", "Alegeți o limbă introducând numărul corespunzător : " );
                ChooseLanguagePrompt.Add( "hu", "Válasszon nyelvet a megfelelő szám megadásával : " );
                ChooseLanguagePrompt.Add( "cs", "Vyberte jazyk zadáním odpovídajícího čísla : " );
                ChooseLanguagePrompt.Add( "sk", "Vyberte jazyk zadaním príslušného čísla : " );
                ChooseLanguagePrompt.Add( "sl", "Izberite jezik z vnosom ustrezne številke : " );
                ChooseLanguagePrompt.Add( "hr", "Odaberite jezik unosom odgovarajućeg broja : " );
                ChooseLanguagePrompt.Add( "sr", "Изаберите језик уношењем одговарајућег броја : " );
                ChooseLanguagePrompt.Add( "bg", "Изберете език, като въведете съответния номер : " );
                ChooseLanguagePrompt.Add( "mk", "Изберете јазик со внесување на соодветниот број : " );
                ChooseLanguagePrompt.Add( "el", "Επιλέξτε γλώσσα εισάγοντας τον αντίστοιχο αριθμό : " );
                ChooseLanguagePrompt.Add( "tr", "İlgili numarayı girerek bir dil seçin : " );
                ChooseLanguagePrompt.Add( "ar", "اختر لغة بإدخال الرقم المقابل : " );
                ChooseLanguagePrompt.Add( "he", "בחר שפה על ידי הזנת המספר המתאים : " );
                ChooseLanguagePrompt.Add( "fa", "با وارد کردن شماره مربوطه یک زبان را انتخاب کنید : " );
                ChooseLanguagePrompt.Add( "hi", "संबंधित संख्या दर्ज करके एक भाषा चुनें : " );
                ChooseLanguagePrompt.Add( "bn", "সংশ্লিষ্ট নম্বর লিখে একটি ভাষা নির্বাচন করুন : " );
                ChooseLanguagePrompt.Add( "ta", "தொடர்புடைய எண்ணை உள்ளிட்டு ஒரு மொழியைத் தேர்ந்தெடுக்கவும் : " );
                ChooseLanguagePrompt.Add( "te", "సంబంధిత సంఖ్యను నమోదు చేసి ఒక భాషను ఎంచుకోండి : " );
                ChooseLanguagePrompt.Add( "th", "เลือกภาษาโดยป้อนหมายเลขที่ตรงกัน : " );
                ChooseLanguagePrompt.Add( "vi", "Chọn một ngôn ngữ bằng cách nhập số tương ứng : " );
                ChooseLanguagePrompt.Add( "id", "Pilih bahasa dengan memasukkan nomor yang sesuai : " );
                ChooseLanguagePrompt.Add( "ms", "Pilih bahasa dengan memasukkan nombor yang sepadan : " );
                ChooseLanguagePrompt.Add( "ko", "해당 번호를 입력하여 언어를 선택하십시오 : " );
                ChooseLanguagePrompt.Add( "ja", "対応する番号を入力して言語を選択してください : " );
                ChooseLanguagePrompt.Add( "zh", "输入对应的数字选择一种语言 : " );
                ChooseLanguagePrompt.Add( "ru", "Выберите язык, введя соответствующий номер : " );
                ChooseLanguagePrompt.Add( "uk", "Виберіть мову, ввівши відповідний номер : " );
                ChooseLanguagePrompt.Add( "be", "Выберыце мову, увёўшы адпаведны нумар : " );
                ChooseLanguagePrompt.Add( "lt", "Pasirinkite kalbą įvesdami atitinkamą numerį : " );
                ChooseLanguagePrompt.Add( "lv", "Izvēlieties valodu, ievadot atbilstošo numuru : " );
                ChooseLanguagePrompt.Add( "et", "Valige keel, sisestades vastava numbri : " );
                ChooseLanguagePrompt.Add( "sq", "Zgjidhni një gjuhë duke futur numrin përkatës : " );
                ChooseLanguagePrompt.Add( "af", "Kies ’n taal deur die ooreenstemmende nommer in te voer : " );
                ChooseLanguagePrompt.Add( "sw", "Chagua lugha kwa kuingiza nambari inayolingana : " );
                ChooseLanguagePrompt.Add( "zu", "Khetha ulimi ngokufaka inombolo ehambisanayo : " );
                ChooseLanguagePrompt.Add( "xh", "Khetha ulwimi ngokufaka inombolo ehambelanayo : " );
                ChooseLanguagePrompt.Add( "am", "ተመሳሳይ ቁጥሩን በመግባት ቋንቋ ይምረጡ : " );
                ChooseLanguagePrompt.Add( "ne", "सम्बन्धित नम्बर प्रविष्ट गरेर भाषा छान्नुहोस् : " );
                ChooseLanguagePrompt.Add( "si", "අදාළ අංකය ඇතුළත් කර භාෂාවක් තෝරන්න : " );
                ChooseLanguagePrompt.Add( "km", "ជ្រើសរើសភាសាដោយបញ្ចូលលេខដែលត្រូវគ្នា : " );
                ChooseLanguagePrompt.Add( "lo", "ເລືອກພາສາໂດຍໃສ່ເລກທີ່ກົງກັນ : " );
                ChooseLanguagePrompt.Add( "mn", "Харгалзах дугаарыг оруулж хэл сонгоно уу : " );
                ChooseLanguagePrompt.Add( "kk", "Сәйкес нөмірді енгізу арқылы тілді таңдаңыз : " );
                ChooseLanguagePrompt.Add( "uz", "Mos raqamni kiritib tilni tanlang : " );
                ChooseLanguagePrompt.Add( "az", "Uyğun nömrəni daxil edərək dili seçin : " );
                ChooseLanguagePrompt.Add( "ka", "აირჩიეთ ენა შესაბამისი ნომრის შეყვანით : " );
                ChooseLanguagePrompt.Add( "hy", "Ընտրեք լեզուն՝ մուտքագրելով համապատասխան համարը : " );
                ChooseLanguagePrompt.Add( "ur", "متعلقہ نمبر درج کر کے زبان منتخب کریں : " );
                ChooseLanguagePrompt.Add( "ps", "د اړوند شمېرې په داخلولو سره ژبه وټاکئ : " );
                ChooseLanguagePrompt.Add( "my", "သက်ဆိုင်ရာ နံပါတ်ကို ထည့်၍ ဘာသာစကားကို ရွေးချယ်ပါ : " );
                ChooseLanguagePrompt.Add( "en", "Choose a language by entering the corresponding number : " );
                ChooseLanguagePrompt.Add( "fr", "Choisissez une langue en entrant le numéro correspondant : " );
                ChooseLanguagePrompt.Add( "es", "Elija un idioma ingresando el número correspondiente : " );
                ChooseLanguagePrompt.Add( "pl", "Wybierz język, wpisując odpowiadający numer : " );
            }
        }

        /// <summary>
        /// Primary code used to load Files, with password protection and a cooldown timer after a certain number of failed attempts.
        /// This code is specifically talored to loading the Transactions file and the Budget file, with some options for if one is
        /// found and not the other, and options to load sample data if the user can't remember their password or doesn't have a file.
        /// Loading sample data or no data still prompts for a password which will be used for the new file saved once they modify
        /// transactions or budgets, this is to keep the flow of the program consistent and to make sure the user has a password set up
        /// even if they don't have a file or can't remember the password for their file.
        /// </summary>
        /// <returns></returns>
        static bool LoadFile() {
            List<Transaction>? transactionFileLoad = null;
            Dictionary<TransactionCategory, decimal>? budgetFileLoad = null;

            int CoolDownAfterXFailedAttemps = 2;
            int attemptMax = 8;
            //has to be initiated first incase user leaves cooldown and comes back!
            string tt = ( ( attemptNumber - CoolDownAfterXFailedAttemps ) * 30 ).ToString( "0.0" );
            string s = messageOutput [ MessageEnum.Label_SForSecond ];
            bool againstTransFile = File.Exists( transactionFileName );
            bool againstBudgetFile = File.Exists( budgetFileName );
            //Loops aslong as the password is not correct and user wants to keep trying.
            while ( !PasswordCorrect ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
                //Exit if taking too long
                if ( attemptNumber > attemptMax ) {
                    ColorConsole.WriteLine( messageOutput [ MessageEnum.ChooseLang_LangApplied ], colorByGroup [ ColorGroup.Header ], ResetColorAfter: false );
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    Console.WriteLine();
                    return false;
                }
                //Password cooldown on and after specified attmpts, timer increases with each new attempt
                if ( attemptNumber > CoolDownAfterXFailedAttemps ) {
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.LoadFile_EnterPwForTransactionFile ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} \n", colorByGroup [ ColorGroup.MenuHeadings ] );
                    Console.Write( messageOutput [ MessageEnum.LoadFile_IncorrectPwCoolDown ] + " " );
                    int cursorLeft = Console.CursorLeft; //used to update time at a specific place on the console
                    int cursorTop = Console.CursorTop;
                    Console.WriteLine( $"\n({messageOutput [ MessageEnum.Label_Press ]} : {messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]}) {messageOutput [ MessageEnum.LoadFile_ToAbortStartNoTrans ]}" );
                    Console.CursorLeft = cursorLeft;
                    Console.CursorTop = cursorTop;
                    //loops the timer with thread sleep (quick and dirty)
                    for ( int e = 0; e < ( attemptNumber - CoolDownAfterXFailedAttemps ) * 300; e += 1 ) {

                        Console.Write( ( e / 10f ).ToString( "0.0" ) + $" ({s}) / {tt} ({s}) : {messageOutput [ MessageEnum.Label_Attempt ]} : {attemptNumber}" );
                        Thread.Sleep( 100 );
                        //Checks for user input to bypass cooldown and load with no transactions, or to try again with another password.
                        if ( Console.KeyAvailable ) {
                            ConsoleKey key = Console.ReadKey( true ).Key;
                            if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                return false;
                            }
                        }
                        Console.CursorLeft = cursorLeft;
                    }
                    Console.Write( $"{tt} ({s}) / {tt} ({s}) \n" );//just shows for a breif moment 
                    Console.Write( "\x1b[3J" ); Console.Clear();
                }
                //Get password If the transaction file exists. Fall back to checking pw for budget.
                if ( againstTransFile ) {
                    password = GetPwd( false );
                    transactionFileLoad = SecureFile.Load<List<Transaction>>( transactionFileName, ref PasswordCorrect, password );
                } else {
                    ColorConsole.WriteLine( $" {transactionFileName} : {messageOutput [ MessageEnum.LoadFile_NoFileFound ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    //No budget file found
                    if ( !againstBudgetFile ) {
                        ColorConsole.WriteLine( $" {budgetFileName} : {messageOutput [ MessageEnum.LoadFile_NoFileFound ]} ", colorByGroup [ ColorGroup.SystemError ] );
                        AnyKeyToContinue( true );
                        return false;
                    } else {
                        //Budget file Found!
                        Console.WriteLine();
                        //LabelFileFound
                        ColorConsole.WriteLine( $" {budgetFileName} : {messageOutput [ MessageEnum.Label_Found ]}! ", ConsoleColor.Black, ConsoleColor.Cyan );
                        Console.WriteLine();
                        //LoadFile_ConfirmLoadingBudgetFileOnly
                        ColorConsole.WriteLine( messageOutput [ MessageEnum.LoadFile_ConfirmLoadingBudgetFileOnly ] );
                        ColorConsole.Write( $"\n({messageOutput [ MessageEnum.SystemInstructions_SpaceOrEnter ]} {messageOutput [ MessageEnum.SystemInstructions_ToLoad ]})", colorByGroup [ ColorGroup.MenuItems ] );
                        ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Or ].ToUpper()} ", ConsoleColor.White );
                        //IntentionaError
                        //    Start here refactir abny Escape or N for escape backspace and dd skip to dictionary
                        ColorConsole.WriteLine( $"({messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]} {messageOutput [ MessageEnum.SystemInstructions_ToSkip ]})", colorByGroup [ ColorGroup.SystemWarning ] );
                        while ( true ) {
                            ConsoleKey key = Console.ReadKey( true ).Key;
                            if ( key == ConsoleKey.Spacebar || key == ConsoleKey.Enter ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                password = GetPwd( false );
                                budgetFileLoad = SecureFile.Load<Dictionary<TransactionCategory, decimal>>( budgetFileName, ref PasswordCorrect, password );
                                break;
                            }
                            if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                                Console.Write( "\x1b[3J" ); Console.Clear();
                                return false;
                            }
                        }
                    }
                    Console.WriteLine();
                }
                //Check the password is correct
                //Password Correct do some checking
                if ( !PasswordCorrect ) {
                    //Sets the cool down timer string for the warning
                    tt = ( ( attemptNumber - CoolDownAfterXFailedAttemps + 1 ) * 30 ).ToString( "0.0" );
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.LoadFile_EnterPwForTransactionFile ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} \n", colorByGroup [ ColorGroup.MenuHeadings ] );
                    ColorConsole.WriteLine( messageOutput [ MessageEnum.LoadFile_PwIncorrect ], colorByGroup [ ColorGroup.SystemWarning ] );
                    ColorConsole.Write( $"\n({messageOutput [ MessageEnum.SystemInstructions_SpaceOrEnter ]}) {messageOutput [ MessageEnum.Label_toTryAgain ]}", colorByGroup [ ColorGroup.MenuItems ] );
                    ColorConsole.Write( $" {messageOutput [ MessageEnum.Label_Or ].ToUpper()} ", ConsoleColor.White );
                    ColorConsole.WriteLine( $"({messageOutput [ MessageEnum.SystemInstructions_EscapeOrBackspace ]}) {messageOutput [ MessageEnum.LoadFile_ForOtherOptionsSampleData ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                    Console.WriteLine();
                    Console.WriteLine( messageOutput [ MessageEnum.LoadFile_ConfrimTryAnotherPw ] );
                    //+1 because the cooldown starts on the next attempt, and no cooldown on the first few attempts
                    if ( attemptNumber + 1 > CoolDownAfterXFailedAttemps && attemptNumber < attemptMax ) {
                        Console.WriteLine();
                        ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.LoadFile_TooManyWrongPwAttempts ]} ({attemptNumber})! {messageOutput [ MessageEnum.LoadFile_CooldownForNextAttempt ]} {tt} ({s}) ", colorByGroup [ ColorGroup.SystemError ] );
                    }
                    //Waits for user input about if they want to try again or not, with the option to bypass the cooldown if they want to wait it out.
                    while ( true ) {
                        ConsoleKey key = Console.ReadKey( true ).Key;
                        //If they want to start with no transactions loaded, break and load with no transactions
                        if ( key == ConsoleKey.Escape || key == ConsoleKey.Backspace ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            attemptNumber++;
                            return false;
                        }
                        //If they want to try again break and loop back to get password
                        if ( key == ConsoleKey.Spacebar || key == ConsoleKey.Enter ) {
                            Console.Write( "\x1b[3J" ); Console.Clear();
                            break;
                        }

                    }
                }
                attemptNumber++;
            }
            BudgetCategories = budgetFileLoad ??
    Enum.GetValues<TransactionCategory>()
        .ToDictionary( cat => cat, cat => 0m );

            if ( transactionFileLoad is null ) {
                Transactions = new List<Transaction>();
                return false;
            }
            Transactions = transactionFileLoad ?? new List<Transaction>();
            return true;
        }

        /// <summary>
        /// Displays a menu that allows the user to choose how to load transaction data, including options to load from
        /// a file, load sample data, or start with no transactions.
        /// </summary>
        /// <remarks>The method clears the console and presents a menu with data loading options. It
        /// continues to prompt the user until a valid selection is made. If the user selects to load sample data or
        /// start with no transactions, a password may be required if not already set. Choosing to start with no
        /// transactions will overwrite any existing data when new transactions or budget information are
        /// added.</remarks>
        static void LoadFileMenu() {
            bool breakout = false;
            bool wrongKey = false;
            Console.Write( "\x1b[3J" ); Console.Clear();
            while ( !breakout ) {
                if ( !wrongKey ) {
                    wrongKey = false;
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]} {messageOutput [ MessageEnum.DataOptions_Header ]} {messageOutput [ MessageEnum.Menu_HeaderOuterDecor ]}", colorByGroup [ ColorGroup.MenuHeadings ] );
                    Console.WriteLine();
                    Console.WriteLine( $"1. {messageOutput [ MessageEnum.DataOptions_LoadFile ]}" );
                    Console.WriteLine( $"2. {messageOutput [ MessageEnum.DataOptions_LoadSample ]}" );
                    Console.WriteLine( $"3. {messageOutput [ MessageEnum.DataOptions_NoloadOrSamples ]}\n" );
                    ColorConsole.WriteLine( $"{messageOutput [ MessageEnum.DataOptions_WarningSavingWithNoDataMayOverwrite ]}", colorByGroup [ ColorGroup.SystemWarning ] );
                }
                switch ( Console.ReadKey( intercept: true ).Key ) {
                    // Load from file
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        breakout = LoadFile();
                        break;
                    // Load sample data with password protection
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        if ( password is null )
                            password = GetPwd( true );
                        SampleTransactionData();
                        AnyKeyToContinue();
                        breakout = true;
                        break;
                    // Start with no transactions or budget, this allows the user to start fresh, but is risky if they accidentally choose
                    // it as it will overwrite loaded transactions and budget when they add or update something.
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Console.Write( "\x1b[3J" ); Console.Clear();
                        if ( password is null ) {
                            password = GetPwd( true );
                        }
                        // Create proper length and values for Budget Catergories based on Transaction Category enum
                        int countBudegetCat = Enum.GetValues<TransactionCategory>().Length;
                        if ( BudgetCategories.Count != countBudegetCat ) {
                            BudgetCategories = Enum.GetValues<TransactionCategory>().ToDictionary( cat => cat, cat => 0m );
                        }
                        breakout = true;
                        break;
                    // Since I rewrite the header this will clear it every loop
                    default:
                        wrongKey = true;
                        break;
                }
            }
            return;
        }

        /// <summary>
        /// Generates a sample set of transaction data for development and testing purposes.
        /// </summary>
        /// <remarks>This method populates the transaction list with randomly generated entries across
        /// various predefined categories and descriptions. It also initializes budget categories if they are not
        /// already set. The generated data is intended for testing and demonstration scenarios, and output is displayed
        /// in the console to indicate the number of transactions created.</remarks>
        private static void SampleTransactionData() {
            Console.WriteLine( messageOutput [ MessageEnum.Sample_Header ] );
            Random rand = new();
            //These are hardcoded because they are for development testing for the professor. No need to keep these moving forward.
            Dictionary<TransactionCategory, string [ ]> expense_disc = new();
            expense_disc.Add( TransactionCategory.Income, new string [ 5 ] { "Salary", "Freelance", "Investment", "Gift", "Other" } );
            expense_disc.Add( TransactionCategory.Housing, new string [ 5 ] { "Rent", "Mortgage", "Property Tax", "Home Insurance", "Maintenance" } );
            expense_disc.Add( TransactionCategory.Groceries, new string [ 5 ] { "Supermarket", "Farmers Market", "Specialty Food Store", "Online Grocery", "Bulk Food Store" } );
            expense_disc.Add( TransactionCategory.Insurance, new string [ 5 ] { "Health Insurance", "Car Insurance", "Home Insurance", "Life Insurance", "Pet Insurance" } );
            expense_disc.Add( TransactionCategory.Debt, new string [ 5 ] { "Credit Card Payment", "Student Loan Payment", "Car Loan Payment", "Personal Loan Payment", "Mortgage Payment" } );
            expense_disc.Add( TransactionCategory.Transportation, new string [ 7 ] { "Gas", "Public Transit", "Ride Sharing", "Car Maintenance", "Parking", "Tolls", "Bicycle Expenses" } );
            expense_disc.Add( TransactionCategory.Entertainment, new string [ 10 ] { "Movie Tickets", "Concert Tickets", "Streaming Service", "Video Games", "Dining Out", "Sports Events", "Hobbies", "Travel", "Books", "Music" } );
            expense_disc.Add( TransactionCategory.Utilities, new string [ 10 ] { "Electricity Bill", "Water Bill", "Gas Bill", "Internet Bill", "Phone Bill", "Trash Service", "Cable TV", "Heating Oil", "Solar Panel Maintenance", "Home Security System" } );
            expense_disc.Add( TransactionCategory.Restaurants, new string [ 10 ] { "Fast Food", "Casual Dining", "Fine Dining", "Coffee Shop", "Takeout", "Food Delivery", "Buffet", "Bar", "Food Truck", "Cafeteria" } );
            expense_disc.Add( TransactionCategory.Healthcare, new string [ 9 ] { "Doctor Visit", "Prescription Medication", "Dental Care", "Vision Care", "Mental Health Services", "Physical Therapy", "Medical Equipment", "Alternative Medicine", "Health Supplements" } );
            expense_disc.Add( TransactionCategory.Fees, new string [ 10 ] { "Bank Service Fee", "Late Payment Fee", "Overdraft Fee", "ATM Fee", "Subscription Fee", "Membership Fee", "License Fee", "Tuition Fee", "Legal Fees", "Consulting Fees" } );
            expense_disc.Add( TransactionCategory.Other, new string [ 10 ] { "Gift", "Donation", "Clothing", "Personal Care", "Miscellaneous", "Education", "Childcare", "Pet Care", "Charity", "Unexpected Expenses" } );
            int count = rand.Next( 1000, 2000 );
            DateOnly start = new( 2020, 1, 1 );
            int daysRange = DateOnly.FromDateTime( DateTime.Today ).DayNumber - start.DayNumber;
            List<TransactionCategory> categories = expense_disc.Keys.ToList();
            for ( int i = 0; i < count; i++ ) {
                TransactionCategory cat = categories [ rand.Next( categories.Count ) ];
                string description = expense_disc [ cat ] [ rand.Next( expense_disc [ cat ].Length ) ];
                DateOnly date = start.AddDays( rand.Next( daysRange ) );
                if ( 1 == 0 ) {
                }
                int num = cat switch {
                    TransactionCategory.Income => rand.Next( 500, 5000 ),
                    TransactionCategory.Housing => rand.Next( 800, 2500 ),
                    TransactionCategory.Groceries => rand.Next( 20, 200 ),
                    TransactionCategory.Utilities => rand.Next( 50, 300 ),
                    TransactionCategory.Transportation => rand.Next( 10, 150 ),
                    TransactionCategory.Restaurants => rand.Next( 10, 120 ),
                    TransactionCategory.Entertainment => rand.Next( 10, 200 ),
                    TransactionCategory.Healthcare => rand.Next( 20, 400 ),
                    TransactionCategory.Insurance => rand.Next( 100, 600 ),
                    TransactionCategory.Debt => rand.Next( 100, 1000 ),
                    TransactionCategory.Fees => rand.Next( 5, 50 ),
                    _ => rand.Next( 10, 300 ),
                };
                if ( 1 == 0 ) {
                }
                decimal amount = num;
                Transactions.Add( new Transaction( date, Math.Round( amount + ( decimal ) rand.NextDouble(), 2 ), description, cat ) );
            }
            if ( BudgetCategories.Count == 0 ) {
                BudgetCategories.Add( TransactionCategory.Housing, 1900m );
                BudgetCategories.Add( TransactionCategory.Groceries, 650m );
                BudgetCategories.Add( TransactionCategory.Transportation, 450m );
                BudgetCategories.Add( TransactionCategory.Utilities, 275m );
                BudgetCategories.Add( TransactionCategory.Restaurants, 300m );
                BudgetCategories.Add( TransactionCategory.Insurance, 325m );
                BudgetCategories.Add( TransactionCategory.Debt, 600m );
                BudgetCategories.Add( TransactionCategory.Entertainment, 250m );
                BudgetCategories.Add( TransactionCategory.Healthcare, 175m );
                BudgetCategories.Add( TransactionCategory.Transfers, 400m );
                BudgetCategories.Add( TransactionCategory.Fees, 100m );
                BudgetCategories.Add( TransactionCategory.Other, 200m );
            }
            Console.WriteLine( $"({count}) {messageOutput [ MessageEnum.Sample_Loaded ]}" );
        }

        /// <summary>
        /// Writes either budget categories or transaction data to a secure file, depending on the specified type.
        /// </summary>
        /// <remarks>If <paramref name="type"/> is set to <see cref="BudgetOrTransaction.Budget"/>, the
        /// method saves budget categories to a secure file. If <paramref name="type"/> is set to <see
        /// cref="BudgetOrTransaction.Transaction"/> and there are no transactions to save, the operation is skipped and
        /// a message is displayed. Otherwise, transactions are saved to a secure file.</remarks>
        /// <param name="type">Specifies whether to write budget categories or transaction data. Use <see
        /// cref="BudgetOrTransaction.Budget"/> to save budget categories, or <see
        /// cref="BudgetOrTransaction.Transaction"/> to save transaction data.</param>
        private static void WriteTransactionsAndBudget( BudgetOrTransaction type ) {
            Console.WriteLine( "" );
            if ( type == BudgetOrTransaction.Budget ) {
                SecureFile.Save( budgetFileName, BudgetCategories, password );
            }
            if ( type == BudgetOrTransaction.Transaction ) {
                if ( Transactions.Count == 0 ) {
                    Console.Write( "\x1b[3J" ); Console.Clear();
                    ColorConsole.WriteLine( $" {messageOutput [ MessageEnum.Write_SkipSaving ]} ", colorByGroup [ ColorGroup.SystemError ] );
                    AnyKeyToContinue();
                    return;
                }
                SecureFile.Save( transactionFileName, Transactions, password );
            }
            Console.CursorLeft = 0;
            ColorConsole.Write( messageOutput [ MessageEnum.Write_Saved ], colorByGroup [ ColorGroup.Header ] );
            ThreadSleepAndClearKeys( 700 );
            Console.CursorLeft = 0;
            //very important to reset due to Write
            ColorConsole.Write( "                 ", colorByGroup [ ColorGroup.Default ] );
            Console.CursorLeft = 0;
        }
        #endregion

        #region >>> /// Helper Methods for sample data and Prompts to pause program flow.
        /// <summary>
        /// Uses threadSleep to pause the screen for a short period of time and clear any ConsoleKeys that may be buffered.
        /// </summary>
        /// <param name="ms">Duration in ms to halt main thread</param>
        /// <param name="clearScreen">True: use Console.Clear() after</param>
        static void ThreadSleepAndClearKeys( int ms = 500, bool clearScreen = false ) {
            Thread.Sleep( ms );
            while ( Console.KeyAvailable )
                Console.ReadKey( true );
            if ( clearScreen ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
            }
        }
        /// <summary>
        /// Animates the logo of the Cornerstone Financial Management System in the console with a multi-pass visual
        /// effect, transitioning from monochrome to color and displaying system information.
        /// </summary>
        /// <remarks>The animation consists of several frames rendered over three passes, each with
        /// distinct color schemes and timing. The system's name and creator details are displayed as part of the
        /// animation. This method is intended for introductory or branding purposes and does not return a
        /// value.</remarks>
        static void introAnimation() {
            ConsoleColor introColorPyrmid = ConsoleColor.Gray;
            ConsoleColor introColorText = ConsoleColor.Gray;
            int introAnimationMs = 160;
            (int Left, int Top) cornersonePos = (0, 0);
            string cornerFMS = "Cornerstone Financial Management System";
            ConsoleColor? pyramidColor = null;
            //Animates the logo for 3 passes, one black and white, another with color and the third is coloring a portion
            for ( int i = 1; i < 4; i++ ) {
                Console.SetCursorPosition( 0, 0 );
                if ( i == 2 ) {
                    introColorPyrmid = ConsoleColor.DarkYellow;
                    introColorText = ConsoleColor.Gray;
                    introAnimationMs = 120;
                }
                if ( i == 3 ) {
                    pyramidColor = ConsoleColor.White;
                    introAnimationMs = 80;
                }
                //Writes portions so the background can be changed, also sets cursor position for the next foreach and while loop for color changing of what is written already.
                ColorConsole.Write( "         ▄█", introColorPyrmid );
                ColorConsole.Write( "▀", introColorPyrmid, ResetColorAfter: true, bg: pyramidColor ); //this gets changed
                ColorConsole.WriteLine( "▄", introColorPyrmid );
                ThreadSleepAndClearKeys( introAnimationMs );
                ColorConsole.Write( "       ▄██", introColorPyrmid );
                ColorConsole.Write( "▀▀▀▀", introColorPyrmid, ResetColorAfter: true, bg: pyramidColor ); // gets changed
                ColorConsole.Write( "▄", introColorPyrmid );
                ColorConsole.Write( "\t\t" );
                cornersonePos = Console.GetCursorPosition();
                ColorConsole.WriteLine( cornerFMS, introColorText );
                introAnimationMs -= 10;
                ThreadSleepAndClearKeys( introAnimationMs );
                ColorConsole.Write( "     ▄███", introColorPyrmid );
                ColorConsole.Write( "▀▀▀▀▀▀▀", introColorPyrmid, ResetColorAfter: true, bg: pyramidColor );//gets changed
                ColorConsole.Write( "▄", introColorPyrmid );
                ColorConsole.WriteLine( "\t    Created By : Eric Beaudoin", introColorText );
                introAnimationMs -= 10;
                ThreadSleepAndClearKeys( introAnimationMs );
                ColorConsole.Write( "   ▄████", introColorPyrmid );
                ColorConsole.Write( "▀▀▀▀▀▀▀▀▀▀", introColorPyrmid, ResetColorAfter: true, bg: pyramidColor );// gets changed
                ColorConsole.Write( "▄", introColorPyrmid );
                ColorConsole.WriteLine( "\t    http://www.CornerPi.com", introColorText );
                introAnimationMs -= 10;
                ThreadSleepAndClearKeys( introAnimationMs );
                ColorConsole.Write( " ▄█████", introColorPyrmid );
                ColorConsole.Write( "▀▀▀▀▀▀▀▀▀▀▀▀▀", introColorPyrmid, ResetColorAfter: true, bg: pyramidColor );// gets changed
                ColorConsole.WriteLine( "▄", introColorPyrmid );

                ThreadSleepAndClearKeys( introAnimationMs );
            }
            pyramidColor = ConsoleColor.White;
            ColorConsole.WriteLine( "Press any key to continue", ConsoleColor.Gray );
            //Blinks a square while waiting
            Console.SetCursorPosition( cornersonePos.Left, cornersonePos.Top );
            foreach ( char e in cornerFMS ) {
                ThreadSleepAndClearKeys( 10 );
                ColorConsole.Write( e.ToString(), ConsoleColor.Cyan );
            }
            bool exitLoopTmp = false;
            //Loops flashing a character background on the buffer window, and if the user presses any key it exits.
            //broke the 500ms into chunks to allow instant reading. with a small time buffer so it doesnt happen to quick.
            while ( true ) {
                for ( int i = 0; i < 50; i++ ) {
                    Thread.Sleep( 10 );
                    if ( Console.KeyAvailable ) {
                        ConsoleKeyInfo key = Console.ReadKey( true );
                        exitLoopTmp = true;
                        break;
                    }
                }

                if ( exitLoopTmp ) {
                    ThreadSleepAndClearKeys( 250 ); // adds a slight delay
                    break;
                }
                Console.SetCursorPosition( 11, 0 );
                pyramidColor = ( pyramidColor == ConsoleColor.Black ) ? ConsoleColor.White : ConsoleColor.Black;
                ColorConsole.Write( "▀", introColorPyrmid, pyramidColor, ResetColorAfter: true );

            }
            #region ///Initializing for formating
            Console.ForegroundColor = colorByGroup [ ColorGroup.Default ] [ 0 ]; //Console Color Default Set
            Console.BackgroundColor = colorByGroup [ ColorGroup.Default ] [ 1 ]; //incase color/information from previous program carried over
            Console.WriteLine( " " ); //helps clear any artifacts from usin colors with Write
            #endregion
        }

        /// <summary>
        /// Prints a reusable message and asks the user to press any key to continue. This is for convenience.
        /// User can also supply a message if they want. Default is to use a dictionary item by enum 
        /// messageOutput[MessageEnum.PressAnyKeyToContinue]
        /// </summary>
        /// <param name="ClearAfter">True if you want to clear the screen after</param>
        /// <param name="BypassMsg">True if you dont want to print the suplpied message</param>
        /// <param name="WriteLine">True if you want this to be on its own line</param>
        public static void AnyKeyToContinue( bool ClearAfter = false, bool BypassMsg = false, bool WriteLine = true, string msg = "", bool DontChangeColor = false ) {

            if ( string.IsNullOrWhiteSpace( msg ) )
                msg = $"({messageOutput [ MessageEnum.Label_Press ]} {messageOutput [ MessageEnum.System_AnyKeyToContinue ]})";
            if ( !BypassMsg )
                if ( WriteLine ) {
                    if ( DontChangeColor == true )
                        ColorConsole.WriteLine( msg, ConsoleColor.Gray );
                    else
                        ColorConsole.WriteLine( msg );
                } else {
                    if ( DontChangeColor == true )
                        Console.Write( msg, ConsoleColor.Gray );
                    else
                        Console.Write( msg );
                }

            Console.ReadKey( true );
            if ( ClearAfter ) {
                Console.Write( "\x1b[3J" ); Console.Clear();
            }
        }

        /// <summary>
        /// //ChatGpt Tool for converting languages quickly, Converts a dictionary to xml. read comments inside
        /// Not currently in use, this is a DEV tool, just like sampleData (which is in use)
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="twoCharLanguageCode"></param>
        /// <param name="filePath"></param>
        private static void ExportLanguageFile() {
            string twoCharLanguageCode = "hi";   // change when generating another language, the last one I did was hindi.

            Dictionary<MessageEnum, string> english = defaultEnglishMessages;
            Dictionary<MessageEnum, string> lang = new() {
                //Translate the enire english dictionary keys and strings exactly to here in a new language. This will format to XML, and you can copy that
                //into the language file. Good for using AI to present data in simple dictionary add statements. AI can manage translations better that way.
                //This method was used to generate the 7 languages seen here. Modifactions can be done and updates using regex directly on the file after, or
                //use this.
            };
            bool error = false;

            if ( english.Count != lang.Count ) {
                Console.WriteLine( $"Dictionary size mismatch! English={english.Count} Lang={lang.Count}" );
                error = true;
            }

            foreach ( var key in english.Keys ) {
                if ( !lang.ContainsKey( key ) ) {
                    Console.WriteLine( $"Missing key: {key}" );
                    error = true;
                }
            }

            foreach ( var key in lang.Keys ) {
                if ( !english.ContainsKey( key ) ) {
                    Console.WriteLine( $"Extra key in language file: {key}" );
                    error = true;
                }
            }

            if ( error ) {
                Console.WriteLine( "\nDictionary mismatch detected. Press any key to halt." );
                Console.ReadKey( true );
                return;
            }

            using StreamWriter writer = new( "language_export.xml" );

            writer.WriteLine( $"<{twoCharLanguageCode}>" );

            string currentGroup = "";

            foreach ( var pair in lang.OrderBy( p => p.Key.ToString() ) ) {
                string key = pair.Key.ToString();
                string value = pair.Value;

                int split = key.IndexOf( '_' );
                string group = split > 0 ? key.Substring( 0, split ) : key;

                if ( group != currentGroup ) {
                    writer.WriteLine( $"<!-- {group} -->" );
                    currentGroup = group;
                }

                writer.WriteLine( $"  <item key=\"{key}\">" );
                writer.WriteLine( $"      <message>{value}</message>" );
                writer.WriteLine( $"  </item>" );
            }

            writer.WriteLine( $"</{twoCharLanguageCode}>" );
        }
        #endregion
    }

    /// <summary>
    /// Defines each transaction the user makes. Has a Date, Category, Description and Amount.
    /// Logical manipulation handled by the pain program class.
    /// </summary>
    [Serializable] //needs to be before transaction class
    public class Transaction {
        public DateOnly Date {
            get; set;
        }
        /// <summary>
        /// Gets or sets the category of the transaction, which determines its classification for reporting and
        /// processing purposes.
        /// </summary>
        /// <remarks>This property allows for categorization of transactions, enabling better organization
        /// and analysis of financial data.</remarks>
        public TransactionCategory Category {
            get; set;
        }
        public decimal Amount {

            get; set;
        }
        /// <summary>
        /// This description is provided by the user and printed when viewing transactions.
        /// </summary>
        public string Description {
            get; set;
        }
        [JsonConstructor]
        public Transaction( DateOnly Date, decimal amount, string Description, TransactionCategory category = 0 ) {
            this.Date = Date;
            //Rounds the users inputed Amount to the nearest hundreth. May not be nesacary but allows users to put in fractional amounts.
            Amount = Math.Round( amount, 2 );
            this.Description = Description;
            this.Category = category;
        }
    }

    /// <summary>
    /// Provides methods for securely loading and saving files, with optional encryption using a password.
    /// </summary>
    /// <remarks>The Load method reads a file from the specified path, decrypting it if a password is
    /// provided. The Save method writes data to a file, encrypting it if a password is supplied. Both methods handle
    /// JSON serialization and deserialization of the data.</remarks>
    public static class SecureFile {
        /// <summary>
        /// Loads an object of type T from the specified file path, optionally decrypting the file using a provided
        /// password.
        /// </summary>
        /// <remarks>If a password is provided, the file is expected to be encrypted and will be decrypted
        /// using the password. If decryption fails due to an incorrect password or file tampering, the method returns
        /// the default value for type T and sets passwordCorrect to <see langword="false"/>. The object is deserialized
        /// from JSON after decryption or direct reading.</remarks>
        /// <typeparam name="T">The type of the object to be loaded from the file.</typeparam>
        /// <param name="path">The path to the file containing the serialized object. The file must exist; otherwise, the method returns
        /// the default value for type T.</param>
        /// <param name="passwordCorrect">A reference parameter that is set to <see langword="true"/> if the password is correct and the file is
        /// successfully decrypted; otherwise, <see langword="false"/>.</param>
        /// <param name="password">An optional password used to decrypt the file. If not provided, the file is loaded without decryption.</param>
        /// <returns>An instance of type T loaded from the file, or the default value of T if the file does not exist or
        /// decryption fails.</returns>
        public static T? Load<T>( string path, ref bool passwordCorrect, string? password = null ) {
            byte [ ] plaintext;
            passwordCorrect = false;
            if ( !File.Exists( path ) )
                return default;
            using var fs = new FileStream( path, FileMode.Open );
            if ( password is not null ) {
                byte [ ] salt = new byte [ 16 ];
                fs.ReadExactly( salt );
                byte [ ] key = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    100_000,
                    HashAlgorithmName.SHA256,
                    32 );
                byte [ ] nonce = new byte [ 12 ];
                fs.ReadExactly( nonce );
                byte [ ] tag = new byte [ 16 ];
                fs.ReadExactly( tag );
                byte [ ] ciphertext = new byte [ fs.Length - 16 - 12 - 16 ];
                fs.ReadExactly( ciphertext );
                plaintext = new byte [ ciphertext.Length ];
                try {
                    using var aes = new AesGcm( key );
                    aes.Decrypt( nonce, ciphertext, tag, plaintext );
                } catch ( CryptographicException ) {
                    return default; // Wrong password or tampered file
                }
            } else {
                plaintext = new byte [ fs.Length ];
                fs.ReadExactly( plaintext );

            }
            string json = Encoding.UTF8.GetString( plaintext );
            passwordCorrect = true;
            return JsonSerializer.Deserialize<T>( json );
        }

        /// <summary>
        /// Saves the specified data to a file in JSON format. If a password is provided, the data is encrypted using AES
        /// before being written to the file.
        /// </summary>
        /// <remarks>When a password is provided, the method generates a random salt and nonce for
        /// encryption. The encrypted output includes the salt, nonce, authentication tag, and ciphertext, written
        /// sequentially to the file. If no password is specified, the data is saved as plain JSON. The caller is
        /// responsible for ensuring the file path is accessible and for managing the password securely.</remarks>
        /// <typeparam name="T">The type of the data to serialize and save.</typeparam>
        /// <param name="path">The file path where the data will be saved. Must be a valid and accessible location.</param>
        /// <param name="data">The data to serialize and save. Can be any object that is supported by JSON serialization.</param>
        /// <param name="password">An optional password used to encrypt the data. If specified, the data is encrypted using AES; otherwise, it
        /// is saved in plain JSON format.</param>
        public static void Save<T>( string path, T data, string? password = null ) {
            string json = JsonSerializer.Serialize( data );
            byte [ ] plainBytes = Encoding.UTF8.GetBytes( json );
            //if the file is using AES use this
            if ( password is not null ) {
                byte [ ] salt = RandomNumberGenerator.GetBytes( 16 );
                byte [ ] key = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    100_000,
                    HashAlgorithmName.SHA256,
                    32 );
                byte [ ] nonce = RandomNumberGenerator.GetBytes( 12 );   // GCM standard
                byte [ ] ciphertext = new byte [ plainBytes.Length ];
                byte [ ] tag = new byte [ 16 ];
                using var aes = new AesGcm( key );
                aes.Encrypt( nonce, plainBytes, ciphertext, tag );
                using var fs = new FileStream( path, FileMode.Create );
                fs.Write( salt );
                fs.Write( nonce );
                fs.Write( tag );
                fs.Write( ciphertext );
            } else {
                using var fs = new FileStream( path, FileMode.Create );
                fs.Write( plainBytes );
            }
        }
    }

    /// <summary>
    /// Simple configuration file for saving the language so the user doesn't have to type it in each time.
    /// Review instances where this is used to ensure when extending nothing is overwritten.
    /// </summary>
    public class Configuration {
        public string language { get; set; } = "en";
        public Configuration( string language = "en" ) {
            this.language = language;
        }
    }

    /// <summary>
    /// Provides static methods for writing messages to the console with customizable foreground and background colors,
    /// supporting both single-line and multi-line output, as well as reading input with color changes.
    /// </summary>
    /// <remarks>The ColorConsole class enables enhanced console output by allowing developers to specify text
    /// and background colors for each message. Methods support optional color resets, user acknowledgment prompts, and
    /// flexible color configuration via parameters or arrays. This class is useful for creating visually distinct
    /// console applications and improving user interaction. All methods are thread-safe for typical console usage, but
    /// concurrent writes may result in interleaved output.</remarks>
    public class ColorConsole {

        /// <summary>
        /// Simply replace Console.Write with this to apply Foreground and Background color. Allows for Write and Write Line.
        /// Can be overloaded with a ConsoleColor array of 2 Count.
        /// </summary>
        /// <param name="msg">String to send to Console.Write(Line)</param>
        /// <param name="fg">Foregorund ConsoleColor</param>
        /// <param name="bg">Background ConsoleColor</param>
        /// <param name="WriteLine">Bool: true uses WriteLine false uses Write</param>
        /// <param name="ResetColorAfter">Resets the Color to before this method. True: resets (default), Fasle: leaves the color change</param>
        /// <param name="ColorAfterFg">Specify Foreground color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="ColorAfterBg">Specify background color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="WaitForAcknowledgment">Include a ReadKey and message that asks for the user to press any key to continue</param>
        public static void Write( string msg, ConsoleColor? fg = null, ConsoleColor? bg = null, bool WriteLine = false, bool ResetColorAfter = true, ConsoleColor? ColorAfterFg = null, ConsoleColor? ColorAfterBg = null, bool WaitForAcknowledgment = false ) {
            ConsoleColor fgReset = Console.ForegroundColor;
            ConsoleColor bgReset = Console.BackgroundColor;
            //only changes gf or background if color is supplied
            if ( fg != null )
                Console.ForegroundColor = ( ConsoleColor ) fg;
            if ( bg != null )
                Console.BackgroundColor = ( ConsoleColor ) bg;
            //simply writes the message depding on the flag. This allows for a method called WriteLine to change this (or manualy set)
            if ( WriteLine )
                Console.WriteLine( msg );
            else
                Console.Write( msg );
            if ( WaitForAcknowledgment ) {
                ColorConsole.WriteLine( $"\n({messageOutput [ MessageEnum.Label_Press ]} : {messageOutput [ MessageEnum.SystemInstructions_AnyKeyToAck ]})", colorByGroup [ ColorGroup.SystemInstructionsGray ] );
                Console.ReadKey( intercept: true );
            }
            if ( ColorAfterFg is not null )
                Console.ForegroundColor = ( ConsoleColor ) ColorAfterFg;
            else
                if ( ResetColorAfter )
                    Console.ForegroundColor = fgReset;
            if ( ColorAfterBg is not null )
                Console.BackgroundColor = ( ConsoleColor ) ColorAfterBg;
            else
                if ( ResetColorAfter )
                    Console.BackgroundColor = bgReset;
            if ( WaitForAcknowledgment )
                Console.Clear();
        }
        /// <summary>
        /// Simply replace Console.Write with this to apply Foreground and Background color. Allows for Write and Write Line.
        /// Can be overloaded with a ConsoleColor array of 2 Count.
        /// </summary>
        /// <param name="msg">String to send to Console.Write(Line)</param>
        /// <param name="colors">ConsoleColor Array of two items used for [0] Foreground ConsoleColor, and [1] Background ConsoleColor (any other indecies ignored)</param>
        /// <param name="ResetColorAfter">Resets the Color to before this method. True: resets (default), Fasle: leaves the color change</param>
        /// <param name="ColorAfterFg">Specify Foreground color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="ColorAfterBg">Specify background color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="WaitForAcknowledgment">Include a ReadKey and message that asks for the user to press any key to continue</param>
        public static void Write( string msg, ConsoleColor [ ] colors, bool ResetColorAfter = true, ConsoleColor? ColorAfterFg = null, ConsoleColor? ColorAfterBg = null, bool WaitForAcknowledgment = false ) {
            //ensure there is an index populated and pass color otherwise use null
            ConsoleColor? fg = ( colors.Length > 0 ) ? colors [ 0 ] : null;
            ConsoleColor? bg = ( colors.Length > 1 ) ? colors [ 1 ] : null;
            Write( msg, fg, bg, WriteLine: false, ResetColorAfter, ColorAfterFg: ColorAfterFg, ColorAfterBg: ColorAfterBg, WaitForAcknowledgment: WaitForAcknowledgment );
        }
        /// <summary>
        /// Simply replace Console.Write with this to apply Foreground and Background color. Allows for Write Line only.
        /// This just calls Write with a true boolean to use WriteLine.
        /// </summary>
        /// <param name="msg">String to send to Console.Write(Line)</param>
        /// <param name="fg">Foregorund ConsoleColor</param>
        /// <param name="bg">Background ConsoleColor</param>
        /// <param name="WriteLine">Bool: true uses WriteLine false uses Write, defualt is WriteLine</param>
        /// <param name="ResetColorAfter">Resets the Color to before this method. True: resets (default), Fasle: leaves the color change</param>
        ///<param name="ColorAfterFg">Specify Foreground color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="ColorAfterBg">Specify background color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="WaitForAcknowledgment">Include a ReadKey and message that asks for the user to press any key to continue</param>
        public static void WriteLine( string msg, ConsoleColor? fg = null, ConsoleColor? bg = null, bool WriteLine = false, bool ResetColorAfter = true, ConsoleColor? ColorAfterFg = null, ConsoleColor? ColorAfterBg = null, bool WaitForAcknowledgment = false ) {
            Write( msg, fg, bg, WriteLine: true, ResetColorAfter: ResetColorAfter, ColorAfterFg: ColorAfterFg, ColorAfterBg: ColorAfterBg, WaitForAcknowledgment: WaitForAcknowledgment );
        }
        /// <summary>
        /// Simply replace Console.Write with this to apply Foreground and Background color. Allows for Write Line only.
        /// This just calls Write with a true boolean to use WriteLine.
        /// </summary>
        /// <param name="msg">String to send to Console.Write(Line)</param>
        /// <param name="colors">ConsoleColor Array of two items used for [0] Foreground ConsoleColor, and [1] Background ConsoleColor (any other indecies ignored)></param>
        /// <param name="ResetColorAfter">Resets the Color to before this method. True: resets (default), Fasle: leaves the color change</param>
        ///<param name="ColorAfterFg">Specify Foreground color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="ColorAfterBg">Specify background color after the line is writen and Awknowledge (if specified) is done.</param>
        ///<param name="WaitForAcknowledgment">Include a ReadKey and message that asks for the user to press any key to continue</param>
        public static void WriteLine( string msg, ConsoleColor [ ] colors, bool ResetColorAfter = true, ConsoleColor? ColorAfterFg = null, ConsoleColor? ColorAfterBg = null, bool WaitForAcknowledgment = false ) {
            ConsoleColor? fg = ( colors.Length > 0 ) ? colors [ 0 ] : null;
            ConsoleColor? bg = ( colors.Length > 1 ) ? colors [ 1 ] : null;
            Write( msg, fg, bg, WriteLine: true, ResetColorAfter, ColorAfterFg: ColorAfterFg, ColorAfterBg: ColorAfterBg, WaitForAcknowledgment: WaitForAcknowledgment );
        }
        /// <summary>
        /// Replaces ReadLine with a ConsoleColor change from an array of 2 ConsoleColors, first is Foregound, second is Bakcground, any other indecies are ignored.
        /// </summary>
        /// <param name="colors">ConsoleColor array, index 0: FG, Index 1: BG</param>
        ///  <param name="returnColorFg">specify Foreground color to change to when done</param>
        /// <param name="returnColorBg">specify Background color to change to when done</param>
        /// <returns>non-null string from ReadLine</returns>
        public static string ReadLine( ConsoleColor [ ] colors, ConsoleColor? returnColorBg = null, ConsoleColor? returnColorFg = null ) {
            ConsoleColor fgReset = Console.ForegroundColor;
            ConsoleColor bgReset = Console.BackgroundColor;
            bool colorsMin2 = false;
            //Protecting reading index when it doesnt exit, sets background only if supplied
            Console.ForegroundColor = colors [ 0 ]!;
            Console.BackgroundColor = colors [ 1 ]!;
            Console.CursorVisible = true;
            string msg = Console.ReadLine()!;
            Console.CursorVisible = false;
            //Change to specified foreground if supplied, if not, use reset if foreground was used, if not leave foregorund alone.
            //State of ConsoleColor.Foreground after readline
            Console.ForegroundColor = returnColorFg is not null ? ( ConsoleColor ) returnColorFg : fgReset;
            //Change to specified background if supplied, if not, use reset if background was used, if not leave background alone.
            //State of ConsoleColor.Background after readline
            if ( returnColorBg is not null )
                Console.BackgroundColor = ( ConsoleColor ) returnColorBg;
            else if ( colorsMin2 )
                Console.BackgroundColor = bgReset;
            return msg;
        }
        /// <summary>
        /// Replaces ReadLine and Overloaded to use no params, or 2 Console Color params, Foreground and Background.
        /// </summary>
        /// <param name="fg">Foreground ConsoleColor</param>
        /// <param name="bg">Background ConsoleColor</param>
        /// <param name="returnColorFg">specify Foreground color to change to when done</param>
        /// <param name="returnColorBg">specify Background color to change to when done</param>
        /// <returns>non-null string from ReadLine</returns>
        public static string ReadLine( ConsoleColor? fgNullable = null, ConsoleColor? bgNullable = null, ConsoleColor? returnColorBg = null, ConsoleColor? returnColorFg = null ) {
            ConsoleColor fg = fgNullable ?? Console.ForegroundColor;
            ConsoleColor bg = bgNullable ?? Console.BackgroundColor;
            var fgbg = new ConsoleColor [ ] { fg, bg };
            return ReadLine( fgbg );
        }
    }
}
