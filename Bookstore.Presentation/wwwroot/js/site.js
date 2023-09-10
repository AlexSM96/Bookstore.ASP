function showPatial(url) {
    const model = document.querySelector('#div-admin')
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => model.innerHTML = response,
        error: (response) => console.log(response)
    })
}

let authorIndex = 1
let categoryIndex = 1
function addAuthor() {
    const author = getAuthorHTML(authorIndex)
    authorIndex++
    $('#author-container').append(author)
}

function addCategory() {
    const category = getCategoryHTML(categoryIndex)
    $('#category-container').append(category)
    categoryIndex++
}

function getAuthorHTML(index) {
    return `
        <div class="form-group">
            <label asp-for="Authors[${index}].Name">${index + 1}.Автор<sup>*</sup></label>
            <input class="form-control" type="text" asp-for="Authors[${index}].Name" name="Authors[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Authors[${index}].Name"></span>
        </div>
    `
}

function getCategoryHTML(index) {
    return `
        <div class="form-group">
            <label asp-for="Categories[${index}].Name">${index + 1}.Категория(жанр)<sup>*</sup></label>
            <input class="form-control" type="text" asp-for="Categories[${index}].Name" name="Categories[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Categories[${index}].Name"></span>
        </div>
        `
}

$(document).ready(function () {
    const commentContainer = document.querySelector('#comment-container')
    const { bookId } = commentContainer.dataset

    $.ajax({
        type: 'POST',
        url: '/Review/GetComments',
        data: { bookId },
        success: (response) => commentContainer.innerHTML = response,
        error: (response) => console.log(response)
    })
})

$(document).ready(function () {
    const dropdownList = document.querySelector('.dropdown-menu') 

    $.ajax({
        type: 'GET',
        url: '/Category/GetCategories',
        success: (response) => dropdownList.innerHTML = response,
        error: (response) => console.log(response)
    })
})

let currentUser;
$(document).ready(async function () {
    const data = await fetch('/User/GetCurrentUser')
    currentUser = await data.json()
})


function createOrder(data) {
    if (currentUser === null || currentUser === undefined) {
        alert("Чтобы совершить покупку необходимо войти в аккаунт или зарегистрироваться");
        return
    }
    $.ajax({
        type: 'POST',
        url: '/Order/CreateOrder',
        data: {
            userId: currentUser.userId,
            bookId: data
        },
        success: () => alert("Заказ создан. Спаибо за покупку!"),
        error: (response) => console.log(response)
    })
}








