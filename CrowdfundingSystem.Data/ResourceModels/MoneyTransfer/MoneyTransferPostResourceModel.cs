namespace CrowdfundingSystem.Data.ResourceModels
{
    public class MoneyTransferPostResourceModel
    {
        public int IdeaId { get; set; }
        public int SenderAccountId { get; set; }
        public decimal TransferredSum { get; set; }
    }
}
