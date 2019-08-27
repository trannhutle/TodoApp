// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const addNewCat = document.querySelector("[data-add-new-todocat]")
const newCatName = document.querySelector("[data-new-cat-name]")

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
        console.log("Call to back-end sucessfully")

    }).fail((result) => {
        alert("Could not add new todo category");
    })
});
