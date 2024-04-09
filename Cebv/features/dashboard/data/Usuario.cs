namespace Cebv.features.dashboard.data;

public class Usuario
{
    public int? id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public DateTime? email_verified_at { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }

    public override string ToString()
    {
        return name;
    }
}