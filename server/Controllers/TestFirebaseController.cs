using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]

public class TestFirebaseController : ControllerBase
{
    private readonly FirebaseClient _firebaseClient;

    public TestFirebaseController()
    {
        _firebaseClient = new FirebaseClient("https://testingdotnetandfirebase-default-rtdb.europe-west1.firebasedatabase.app/");
    }

    [HttpGet("test")]
    public async Task<IActionResult> TestFirebase()
    {
        try
        {
            var result = await _firebaseClient
                .Child("test")
                .PostAsync(new { message = "Hello, Firebase!" });

            return Ok(new { Success = true, Result = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Message = ex.Message });
        }
    }
}
