using UnityEngine;
using Supabase;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
[CreateAssetMenu(fileName = "SupabaseManager", menuName = "SupabaseManager")]
public class SupabaseManager : ScriptableObject
{

    private Client supabaseClient;
    public Client GetClient() => supabaseClient;

    private const string supabaseUrl = "https://edmkfjdowivfcoacemfs.supabase.co";
    [SerializeField] private VariableReference<string> supabaseKey;


    private async void OnEnable()
    {
        await InitializeSupabase();
    }

    private async Task InitializeSupabase()
    {
        supabaseClient = new Client(supabaseUrl, supabaseKey);
        await supabaseClient.InitializeAsync();
        if (supabaseClient.Auth != null)
        {
            Debug.Log("Supabase client initialized successfully.");
        }
    }
}
