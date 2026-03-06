namespace FinanceTracker.Core.Models.Forms
{
    public enum FormActionCode
    {
        Save = 1,
    }

    public class FormAction
    {
        public FormActionCode Code { get; set; }
    }
}