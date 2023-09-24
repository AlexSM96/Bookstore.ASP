function showPatial(url) {
    const model = document.querySelector('#div-admin')
    $.ajax({
        type: 'GET',
        url: url,
        success: (response) => model.innerHTML = response,
        error: (response) => console.log(response)
    })
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

    let books = JSON.parse(data)
    let price = 0
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

let booksId = []
function getBooksId(id) {
    if (!booksId.includes(id)) {
        booksId.push(id)
    }
}

function addToBasket(id) {
    getBooksId(id)

    $.ajax({
        type: 'POST',
        url: '/Basket/AddToBasket',
        data: { booksId },
        error: (response) => console.log(response)
    })
}









