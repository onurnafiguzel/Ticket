using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Constants
{
    public static class Messages
    {
        public static string AdminAdded = "Admin eklendi.";
        public static string AdminDeleted = "Admin silindi.";
        public static string AdminNotFound = "Admin bulunamadı.";
        public static string AdminUpdated = "Admin güncellendi.";

        public static string CustomerAdded = "Müşteri eklendi.";
        public static string CustomerDeleted = "Müşteri silindi.";
        public static string CustomerNotFound = "Müşteri bulunamadı.";
        public static string CustomerUpdated = "Müşteri güncellendi.";
        public static string CustomerAlreadyExists = "Kullanıcı zaten mevcut.";

        public static string FilmAdded = "Film eklendi.";
        public static string FilmDeleted = "Film silindi.";
        public static string FilmNotFound = "Film bulunamadı.";
        public static string FilmUpdated = "Film güncellendi.";

        public static string PasswordError = "Parola hatalı.";
        public static string SuccesfullLogin = "Giriş başarılı.";
        public static string UserExist = "Kullanıcı mevcut.";
        public static string UserRegistered = "Kullanıcı kaydedildi.";
        public static string UserUpdated = "Kullanıcı değiştirildi.";
        public static string AccessTokenCreated = "AccessToken oluşturuldu.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string CastsShowed = "Castlar gösterildi.";
        public static string CastsNotFound = "Cast bulunamadı.";

        public static string PlaceNotFound = "Place bulunamadı.";
        public static string TheatherNotFound = "Theather bulunamadı.";
        public static string SessionNotFound = "Session bulunamadı.";
        public static string GenreNotFound = "Türler bulunamadı.";
        public static string ActorsNotFound = "Actorler bulunamadı.";
    }
}
