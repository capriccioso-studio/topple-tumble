using Firebase.Firestore;
using Firebase.Extensions;

[FirestoreData]
public class UserClass
{
        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string Username { get; set; }

        [FirestoreProperty]
        public string ProfilePicture { get; set; }

        [FirestoreProperty]
        public int Score { get; set; }

        [FirestoreProperty]
        public int Drops { get; set; }
        
        [FirestoreProperty]
        public int PendingClaim { get; set; }
}