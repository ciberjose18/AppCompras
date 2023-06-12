using AppCompras.Conexiones;
using AppCompras.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCompras.Datos
{
    public class Dproductos
    {
        public async Task <List<Mproductos>> MostrarProductos()
        {
            return (await Cconexion.firebase.Child("Productos").OnceAsync<Mproductos>()).Select(item => new Mproductos
            {
                Descripcion = item.Object.Descripcion,
                Icono = item.Object.Icono,
                Precio = item.Object.Precio,
                Peso = item.Object.Peso,
                Idproducto = item.Key
            }).ToList();
        }

        public async Task<List<Mproductos>> MostrarproductosXid(Mproductos parametros)
        {
            return (await Cconexion.firebase
                .Child("Productos")
                .OnceAsync<Mproductos>())
                .Where(a => a.Key == parametros.Idproducto).Select(item => new Mproductos
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Peso = item.Object.Peso,
                    Idproducto = item.Key
                }).ToList();
        }
    }
}
