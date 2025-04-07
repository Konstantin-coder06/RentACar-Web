document.getElementById('imageUpload').addEventListener('change', function (e) {
    var files = e.target.files;
    var container = document.getElementById('imagePreviewContainer');
    container.innerHTML = '';
    Array.from(files).forEach((file, index) => {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement('img');
            img.src = e.target.result;
            img.classList.add('img-thumbnail', 'm-2');
            img.setAttribute('data-original-index', index);
            container.appendChild(img);
            updateImageOrder();
        };
        reader.readAsDataURL(file);
    });
});

var container = document.getElementById('imagePreviewContainer');
var sortable = Sortable.create(container, {
    animation: 150,
    onSort: function (evt) {
        updateImageOrder();
    }
});

function updateImageOrder() {
    const order = Array.from(document.querySelectorAll('#imagePreviewContainer img'))
        .map(img => img.getAttribute('data-original-index'));
    document.getElementById('imageOrder').value = order.join(',');
}

document.querySelector('form').addEventListener('submit', function (e) {
    updateImageOrder();
});