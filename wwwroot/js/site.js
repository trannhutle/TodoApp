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
const remainingTodo = document.querySelector("[data-remaining-todo]")
const deleteStuffTemplate = document.getElementById("delete-stuff-template")
const deleteStuffContainer = document.querySelector("[data-delete-stuff]")

const successCode = 200
const TODO_CAT_ID_PREFIX = "todo-cat-id-"
const REMAINING_TEXT = "Remaining tasks"


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
    if (isTodoValid()) {
        addTodo()
    }
})

newTodoName.addEventListener("keypress", e => {
    const code = (e.keyCode ? e.keyCode : e.which);
    if (code == 13) {
        e.preventDefault()
        addNewTodo.click()
    }
})

todoListContainer.addEventListener("click", e => {
    if (e.target.tagName.toLowerCase() == "input") {
        const selectedTask = e.target.value
        const complete = e.target.checked
        updateTodo(selectedTask, complete)
    }
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

function isTodoValid() {
    const todoName = newTodoName.value;
    if (todoName == null || todoName.trim() === "") {
        return false
    }
    if (todoName.trim().length > 100) {
        alert("Todo is not over than 100 character")
        return false
    }
    return true
}

function addTodo() {
    const name = newTodoName.value;
    const todoCatId = newTodoName.dataset.curCatId
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
            getTodoList(result => {
                clearElement(todoListContainer)
                updateRemainingTodo(result)
                renderTodoList(result)
                addDeleteStuff(result)
            })
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
}

// Update todo status
function updateTodo(todoId, complete) {

    const t = { ID: parseInt(todoId), Complete: complete }

    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "POST",
        url: "/Index?handler=UpdateTodo",
        type: "application/json",
        data: t,
    }).done((result) => {
        const statusCode = result.statusCode;
        if (statusCode === successCode) {
            // Refresh the list
            getTodoList(result => {
                updateRemainingTodo(result)
            })
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
}

// Get to do list of todo category
function getTodoList(callback) {
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
            callback(todoList)
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

function addDeleteStuff(todoList) {
    if (todoList && todoList.length > 0) {
        const deleteStuffElement = document.importNode(deleteStuffTemplate.content, true);
        clearElement(deleteStuffContainer)
        deleteStuffContainer.appendChild(deleteStuffElement);
    }
}

// Render toto category to html template
function renderTodoCatList(todoCat) {
    const newTodoCatElement = document.createElement("li")
    newTodoCatElement.dataset.listId = TODO_CAT_ID_PREFIX + todoCat.id
    newTodoCatElement.classList.add("list-name")
    newTodoCatElement.innerText = todoCat.name
    todoCatList.appendChild(newTodoCatElement)
}

// Update remaining todo list number
function updateRemainingTodo(todoList) {
    if (todoList && todoList.length > 0) {
        var count = 0
        todoList.forEach(i => {
            if (i.complete) {
                count++
            }
        })
        const remain = todoList.length - count
        if (count > 0) {
            remainingTodo.innerText = REMAINING_TEXT + " " + remain
        } else {
            remainingTodo.innerText = ""
        }
    } else {
        remainingTodo.innerText = ""
    }
}

// Clear unused elements
function clearElement(element) {
    while (element.firstChild) {
        element.removeChild(element.firstChild)
    }
}
