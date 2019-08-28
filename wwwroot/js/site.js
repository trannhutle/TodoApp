// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const addNewCat = document.querySelector("[data-add-new-todocat]")
const newCatName = document.querySelector("[data-new-cat-name]")
const todoCatList = document.querySelector("[data-todo-cat-list]")
const addNewTodo = document.querySelector("[data-add-new-todo]")
const newTodoName = document.querySelector("[data-new-todo-name]")
const todoTemplate = document.getElementById("todo-template")
const todoListContainer = document.querySelector("[data-todo-list-container]")

const successCode = 200
const TODO_CAT_ID_PREFIX = "todo-cat-id-"

addNewCat.addEventListener("click", e => {
    e.preventDefault();
    const catName = newCatName.value;
    if (catName == null || catName.trim() === "") {
        return
    }
    if (catName.trim().length > 20) {
        alert("Todo category is not over 20 character")
        return
    }
    addTodoCategory(catName)
});

addNewTodo.addEventListener("click", e => {
    e.preventDefault()
    const todoName = newTodoName.value;
    const curTodoCatId = newTodoName.dataset.curCatId
    if (todoName == null || todoName.trim() === "") {
        return
    }
    if (todoName.trim().length > 100) {
        alert("Todo is not over than 100 character")
        return
    }
    addTodo(todoName, curTodoCatId)
})

// reset input boxes
function clearInput() {
    newTodoName.value = ""
    newCatName.value = ""
}

function addTodoCategory(name) {
    // Send the request to back-end
    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "POST",
        url: "/Index?handler=AddNewCategory",
        data: {
            "catName": name
        },
    }).done((result) => {
        const statusCode = result.statusCode;
        if (statusCode === successCode) {
            clearInput()
            const newTodoCat = result.data
            renderTodoCatList(newTodoCat)
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
}

function addTodo(name, todoCatId) {
    // Send the request to back-end
    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "POST",
        url: "/Index?handler=AddNewTodo",
        data: {
            "todoName": name,
            "catId": todoCatId
        },
    }).done((result) => {
        const statusCode = result.statusCode;
        if (statusCode === successCode) {
            clearInput()
            //Get new todo list
            getTodoList()
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
}

// Get to do list of todo category
function getTodoList() {
    const catId = newTodoName.dataset.curCatId
    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "GET",
        url: "/Index?handler=TodoList",
        data: {
            "catId": catId
        },
    }).done((result) => {
        const statusCode = result.statusCode;

        if (statusCode === successCode) {
            const todoList = result.data
            renderTodoList(todoList)
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
}

// Render todo list to html template
function renderTodoList(todoList) {
    todoList.forEach(todo => {
        const todoElement = document.importNode(todoTemplate.content, true)
        const checkBox = todoElement.querySelector("input")
        checkBox.id = todo.id;
        checkBox.checked = todo.complete;
        const label = todoElement.querySelector("label")
        label.htmlFor = todo.id
        label.append(todo.name)
        todoListContainer.appendChild(todoElement)
    })
}

function renderTodoCatList(todoCat) {
    const newTodoCatElement = document.createElement("li")
    newTodoCatElement.dataset.listId = TODO_CAT_ID_PREFIX + todoCat.id
    newTodoCatElement.classList.add("list-name")
    newTodoCatElement.innerText = todoCat.name
    todoCatList.appendChild(newTodoCatElement)
}