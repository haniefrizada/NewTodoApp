using Microsoft.AspNetCore.Mvc;
using NewTodoApp.Models;
using Newtonsoft.Json;
using System.Text;

public class TodoItemController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress = "https://jsonplaceholder.typicode.com/todos";

    public TodoItemController()
    {
        _httpClient = new HttpClient();
    }

    // GET: Todo
    public async Task<ActionResult> Index()
    {
        var response = await _httpClient.GetAsync(_baseAddress);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(jsonString);

            return View(todoItems);
        }

        return View();
    }

    // GET: Todo/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Todo/Create
    [HttpPost]
    public async Task<ActionResult> Create(TodoItem todoItem)
    {
        var json = JsonConvert.SerializeObject(todoItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_baseAddress, content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View(todoItem);
    }

    // GET: Todo/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseAddress}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var todoItem = JsonConvert.DeserializeObject<TodoItem>(jsonString);

            return View(todoItem);
        }

        return RedirectToAction("Index");
    }

    // POST: Todo/Edit/5
    [HttpPost]
    public async Task<ActionResult> Edit(int id, TodoItem todoItem)
    {
        var json = JsonConvert.SerializeObject(todoItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{_baseAddress}/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return View(todoItem);
    }

    // GET: Todo/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseAddress}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var todoItem = JsonConvert.DeserializeObject<TodoItem>(jsonString);

            return View(todoItem);
        }

        return RedirectToAction("Index");
    }

    // POST: Todo/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseAddress}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Delete", new { id = id });
    }
}
