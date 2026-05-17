using Projeto360.Dominio.Entidades;
using Projeto360.Servicos.interfaces;
using Projeto360.Servicos.JsonPlaceHolder.JsonPlaceHolderServico.Models;
using Newtonsoft.Json;


public class JsonPlaceHolderServico : IJsonPlaceHolderServico
{   
    private readonly HttpClient _httpCliente;

    public JsonPlaceHolderServico()
    {
        _httpCliente = new HttpClient();
        _httpCliente.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

    }

   
    public async Task<List<Tarefa>> ListarTarefas()
    {
        HttpResponseMessage response = await _httpCliente.GetAsync("todos");
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        var todo = JsonConvert.DeserializeObject<List<todo>>(responseBody);


        
        var tarefas = todo.Select(todo => new Tarefa
        {
            ID = todo.id,
            Nome = todo.title,
            Completa = todo.completed
        }
        
        
        ).ToList();

        return tarefas;

        
    }
    
}

