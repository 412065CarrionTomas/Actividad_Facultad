



using Actividad_Facultad.Domain;
using Actividad_Facultad.Service;

ArticleService articleService = new ArticleService();

List<Article> la = articleService.GetArticles();

//OBTENER TODOS LOS ARTICULOS
if(la.Count > 0)
{
    foreach(Article a in la)
    {
        Console.WriteLine(a);
    }
}
else
{
    Console.WriteLine("No hay  articulos");
}
Console.WriteLine("FIN");

//OBTENER ARTICULOS POR ID
Console.WriteLine("OBTENER ARTICULOS POR ID");
int id = Int32.Parse(Console.ReadLine());
Article? article = articleService.GetArticleById(id);
if (article != null)
{
    Console.WriteLine($"Producto encontrado: {article}");
}
else
{
    Console.WriteLine("no hay articulo por id");
}
Console.WriteLine("FIN");

//ACTUALIZAR PRODUCTO O CREER SI NO EXISTE
Console.WriteLine("PARTE SAVE");
Console.WriteLine("ingrese id del articulo");
int artiID = Int32.Parse(Console.ReadLine());
Console.WriteLine("ingrese descripcion del articulo");
string artiDescrip = Console.ReadLine();
Article article1 = new Article()
{
    ArticuloID = artiID,
    Descripcion = artiDescrip
};
int resultado = articleService.articleSave(article1);
if(resultado == 1)
{
    Console.WriteLine("insercion realizada con exito");
}
else if(resultado == 2)
{
    Console.WriteLine("actualizacion con exito");
}
else if(resultado == -1)
{
    Console.WriteLine("error al save");
}
Console.WriteLine("FIN");

//BORRAR PRODUCTO
Console.WriteLine("Ingrese codigo de articulo a eliminar.");
int idEliminar = int.Parse(Console.ReadLine());
bool result = articleService.articleDelete(idEliminar);
if (result != false)
    Console.WriteLine($"Producto dado de baja con exito: {id}");
else
    Console.WriteLine($"No se encontro el producto con ID = {id}");