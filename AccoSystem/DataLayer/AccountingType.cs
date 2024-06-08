namespace AccoSystem.DataLayer;

public partial class AccountingType
{
    public int TypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public virtual ICollection<Accounting> Accountings { get; set; } = new List<Accounting>();
}
