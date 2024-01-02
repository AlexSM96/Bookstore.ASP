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

    const order = JSON.parse(data)
    const books = order.$values
    const totalPrice = document.querySelector('#totalPrice')
    let bodyHtml = books.map(book => toHtml(book)).join(' ')
    bodyHtml = bodyHtml + priceHtml(totalPrice)

    $.ajax({
        type: 'POST',
        url: '/Order/CreateOrder',
        data: {
            model: {
                userid: currentUser.userId,
                books: books,
            },
            bodyHtml: bodyHtml
        },
        success: () => {
            location.reload()
            alert("Заказ создан. Вам на почту отправлены информация по заказу. Спасибо за покупку!")
        },
        error: (response) => console.log(response)
    })
}

function priceHtml(totalPrice) {
    return `
        <div>
            <p>Общая стоимость заказа:</p>
            <h2>${totalPrice.textContent}</h2>
        </div>
    `
}

function toHtml(book) {
    return `<h3>${book.title}</h3>`
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

function removeAuthor(index) {
    const divAuthor = document.querySelector(`#Author-${index}`)
    divAuthor.parentNode.removeChild(divAuthor)
}

function removeCategory(index) {
    const divCategory = document.querySelector(`#Category-${index}`)
    divCategory.parentNode.removeChild(divCategory)
}

function addCategory() {
    const category = getCategoryHTML(categoryIndex)
    $('#category-container').append(category)
    categoryIndex++
}

function getAuthorHTML(index) {
    return `
        <div id="Author-${index}" class="form-group">
            <label asp-for="Authors[${index}].Name">Автор<a class="text-decoration-none" title="Удалить" alt="Удалить" onclick="removeAuthor('${index}')">&#10060;</a></label>
            <input class="form-control" type="text" asp-for="Authors[${index}].Name" name="Authors[${index}].Name" />
            <span class="validation-message" asp-validation-for="Authors[${index}].Name"></span>
        </div>
    `
}

function getCategoryHTML(index) {
    return `
        <div id="Category-${index}" class="form-group">
            <label asp-for="Categories[${index}].Name">Категория(жанр)<a class="text-decoration-none" title="Удалить" alt="Удалить" onclick="removeCategory('${index}')">&#10060;</a></label>
            <input class="form-control" type="text" asp-for="Categories[${index}].Name" name="Categories[${index}].Name"/>
            <span class="validation-message" asp-validation-for="Categories[${index}].Name"></span>
        </div>
    `
}








