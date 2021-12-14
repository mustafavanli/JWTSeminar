namespace JWTSeminar
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(UserCrendetial user);
    }
}
