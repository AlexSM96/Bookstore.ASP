$(document).ready(function () {
    const commentContainer = document.querySelector('#comment-container')
    if (commentContainer === undefined || commentContainer === null) {
        return
    }
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
    const dropdownList = document.querySelector('#categoryDropDown')
    if (dropdownList === undefined || dropdownList === null) {
        return
    }
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

    let price = 0
    books = Array.from(data)
    let totalPrice = books.reduce((acc, book) => acc + book.Price, price)
    let body = books.map(toHtml).join(' ')
    body = body + priceHtml(totalPrice)

    $.ajax({
        type: 'POST',
        url: '/Order/CreateOrder',
        data: {
            model: {
                userId: currentUser.userId,
                books: books,
            },
            bodyHtml: body
        },
        success: () => alert("Заказ создан.Вам на почту отправлены информация по заказу. Спаибо за покупку!"),
        error: (response) => console.log(response)
    })
}

function priceHtml(totalPrice) {
    return `
        <li class="list-group-item">
            <p>Общая стоимость заказа:</p>
            <h2>${totalPrice} Руб.</h2>
        </li>`
}

function toHtml(book) {
    return `
        <li class="list-group-item">
            <h3>Название: ${book.Title}</h3>
        </li>`
}

function addToBasket(bookId) {
    $.ajax({
        type: 'POST',
        url: '/Basket/AddToBasket',
        data: { bookId },
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
            <label asp-for="Authors[${index}].Name">Автор № ${index + 1}</label>
            <input class="form-control" type="text" asp-for="Authors[${index}].Name" name="Authors[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Authors[${index}].Name"></span>
        </div>
    `
}

function getCategoryHTML(index) {
    return `
        <div class="form-group">
            <label asp-for="Categories[${index}].Name">Категория(жанр) № ${index + 1}</label>
            <input class="form-control" type="text" asp-for="Categories[${index}].Name" name="Categories[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Categories[${index}].Name"></span>
        </div>
        `
}








