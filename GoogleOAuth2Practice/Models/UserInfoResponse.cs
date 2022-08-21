namespace GoogleOAuth2Practice.Models
{
    public class UserInfoResponse
    {
       //{
       //   "sub": "111451842018960880832",
       //   "name": "tai khoan kiem tra",
       //   "given_name": "tai khoan",
       //   "family_name": "kiem tra",
       //   "picture": "https://lh3.googleusercontent.com/a/AItbvmlByLd2BwhBlSGXHXSscCL_8uBSSqctAID8i3jo\u003ds96-c",
       //   "email": "taikhoankiemtra814@gmail.com",
       //   "email_verified": true,
       //   "locale": "en"
       // }

        public string? sub { get; set; }
        public string? name { get; set; }
        public string? given_name { get; set; }
        public string? family_name { get; set; }
        public string? email { get; set; }
        public string? picture { get; set; }
        public string? email_verified { get; set; }
        public string? locale { get; set; }
    }
}
