// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const addNewCat = document.querySelector("[data-add-new-todocat]")
const newCatName = document.querySelector("[data-new-cat-name]")
const todoCatList = document.querySelector("[data-todo-cat-list]")
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

//todoCatList.addEventListener("click", e => {
//    if (e.target.tagname.toLowerCase() == "li") {
//        e.target.dataset.listId = 
//    }
//})

//function reloadList() {
//    todoCatList.
//}