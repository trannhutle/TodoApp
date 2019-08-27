// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const addNewCat = document.querySelector("[data-add-new-todocat]")
const newCatName = document.querySelector("[data-new-cat-name]")
const todoCatList = document.querySelector("[data-todo-cat-list]")
const addNewTodo = document.querySelector("[data-add-new-todo]")
const newTodoTitle = document.querySelector("[data-new-todo-title]")

const successCode = 200
const TODO_CAT_ID_PREFIX = "todo-cat-id-"

addNewCat.addEventListener("click", e => {
    e.preventDefault();
    const catName = newCatName.value;

    // Check if input data is null
    if (catName == null || catName === "") {
        return
    }
    // Send the request to back-end
    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "POST",
        url: "/Index?handler=AddNewCategory",
        data: {
            "catName": catName
        },
    }).done((result) => {
        const statusCode = result.statusCode;
        if (statusCode === successCode) {
            newCatName.value = ""
            console.log("Call to back-end sucessfully")
            const newTodoCat = result.data
            const newTodoCatElement = document.createElement("li")
            newTodoCatElement.dataset.listId = TODO_CAT_ID_PREFIX + newTodoCat.id
            newTodoCatElement.classList.add("list-name")
            newTodoCatElement.innerText = newTodoCat.name
            todoCatList.appendChild(newTodoCatElement)
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
});

addNewTodo.addEventListener("click", e => {
    e.preventDefault()
    const todoTitle = newTodoTitle.value;
    const curTodoCatId = newTodoTitle.dataset.curCatId

    if (todoTitle == null || todoTitle === "") {
        return
    }
    // Send the request to back-end
    $.ajax({
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        method: "POST",
        url: "/Index?handler=AddNewTodo",
        data: {
            "todoTitle": todoTitle,
            "catId": curTodoCatId
        },
    }).done((result) => {
        const statusCode = result.statusCode;
        if (statusCode === successCode) {
            newTodoTitle.value = ""
            console.log("Call to back-end sucessfully")
        } else {
            alert("Could not add new todo category");
        }
    }).fail((result) => {
        alert("Could not add new todo category");
    })
})