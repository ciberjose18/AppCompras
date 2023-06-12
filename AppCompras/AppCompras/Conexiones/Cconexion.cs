using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCompras.Conexiones
{
    public class Cconexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://appcompras-d437d-default-rtdb.firebaseio.com/");
    }
}
