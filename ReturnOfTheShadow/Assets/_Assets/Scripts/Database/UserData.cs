using UnityEngine;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
[Table("user_Data")]
public class UserData : BaseModel
{
    [PrimaryKey("user_DataID",false)]
    public string User_DataID { get; set; }
    [Column("Coins")]
    public int Coins { get; set; }
}
