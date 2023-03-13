using Firebase.Firestore;
using Firebase.Extensions;

[FirestoreData]
public class ClaimLogClass
{
        [FirestoreProperty]
        public string Username { get; set; }

        [FirestoreProperty]
        public string TransactionHash { get; set; }

        [FirestoreProperty]
        public string Token { get; set; }

        [FirestoreProperty]
        public string Amount { get; set; }

}