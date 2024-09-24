public class LoginService
{
    public bool AuthenticateUser(LoginRequest loginRequest)
    {

        // loginRequest.Username;
        // loginRequest.Password;
        // if not in DB return false;

        return false;
    }

    public bool IsLoggedIn(IsLoggedInRequest request){
        
        // if sessionId not in session
        // return false;
        
        return true;
    }

    public bool LogoutUser(LogoutRequest logoutRequest){

        // logoutRequest.SessionId
        // TODO: Remove session ID
        // IF FAILS: return false;

        return true;
    }

}