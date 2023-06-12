using AppCompras.Conexiones;
using AppCompras.Modelo;
using Firebase.Database.Query;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Xamarin.Forms;

namespace AppCompras.Datos
{
    public class Ddetallescompras
    {
        public async Task InsertarDc(Mdetallecompras parametros)
        {
            await Cconexion.firebase
                .Child("DetalleCompra")
                .PostAsync(new Mdetallecompras()
                {
                    Cantidad = parametros.Cantidad,
                    Idproducto = parametros.Idproducto,
                    Preciocompra = parametros.Preciocompra,
                    Total = parametros.Total
                });
        }

        public async Task<List<Mdetallecompras>> MostrarVistapreviaDc()
        {
            var ListaDc = new List<Mdetallecompras>();
            var funcionproductos = new Dproductos();
            var parametrosProductos = new Mproductos();
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new Mdetallecompras
                {
                    Idproducto = item.Object.Idproducto,
                    Iddetallecompra = item.Key
                    ,
                    Cantidad = item.Object.Cantidad,
                    Total = item.Object.Total
                })
                ;

            foreach (var hobit in data)
            {
                var parametros = new Mdetallecompras();
                parametros.Idproducto = hobit.Idproducto;
                parametrosProductos.Idproducto = hobit.Idproducto;
                var listaproductos = await funcionproductos.MostrarproductosXid(parametrosProductos);

                parametros.Imagen = listaproductos[0].Icono;

                ListaDc.Add(parametros);


            }
            return ListaDc;
        }

        public async Task<List<Mdetallecompras>> MostrarDc()
        {
            var ListaDc = new List<Mdetallecompras>();
            var parametrosProductos = new Mproductos();
            var funcionproductos = new Dproductos();
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new Mdetallecompras
                {
                    Idproducto = item.Object.Idproducto,
                    Iddetallecompra = item.Key
                    ,
                    Cantidad = item.Object.Cantidad,
                    Total = item.Object.Total
                })
                ;

            foreach (var hobit in data)
            {
                var parametros = new Mdetallecompras();
                parametros.Idproducto = hobit.Idproducto;
                parametrosProductos.Idproducto = hobit.Idproducto;
                var listaproductos = await funcionproductos.MostrarproductosXid(parametrosProductos);
                parametros.Descripcion = listaproductos[0].Descripcion;
                parametros.Imagen = listaproductos[0].Icono;
                parametros.Cantidad = hobit.Cantidad;
                parametros.Total = hobit.Total;

                ListaDc.Add(parametros);
            }
            return ListaDc;

        }

        public async Task<List<Mdetallecompras>> MostrarDcXidProducto(string idproducto)
        {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<Mdetallecompras>()
                ).Where(a => a.Object.Idproducto == idproducto).Select(item => new Mdetallecompras
                {
                    Total = item.Object.Total
                }).ToList();

        }

        public async Task Editardetalle(Mdetallecompras parametros)
        {
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Object.Idproducto == parametros.Idproducto).FirstOrDefault();
            double cantidadInicial = Convert.ToDouble(data.Object.Cantidad);
            data.Object.Cantidad = (cantidadInicial + Convert.ToDouble(parametros.Cantidad)).ToString();
            double cantidad = Convert.ToDouble(data.Object.Cantidad);
            double preciocompra = Convert.ToDouble(parametros.Preciocompra);
            double total = 0;
            total = cantidad * preciocompra;
            data.Object.Total = total.ToString();
            await Cconexion.firebase
                .Child("DetalleCompra")
                .Child(data.Key)
                .PutAsync(data.Object);

        }

        public async Task<List<Mdetallecompras>> Mostrarcantidades()
        {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<Mdetallecompras>()
                ).Where(a => a.Key != "Modelo").Select(item => new Mdetallecompras
                {
                    Cantidad = item.Object.Cantidad
                }
                ).ToList();
        }

        public async Task<string> Sumarcantidad()
        {

            var funcion = new Ddetallescompras();
            var lista = await funcion.Mostrarcantidades();
            double cantidad = 0;
            foreach (var hobit in lista)
            {
                cantidad += Convert.ToDouble(hobit.Cantidad);

            }
            return cantidad.ToString();

        }
    }
}
