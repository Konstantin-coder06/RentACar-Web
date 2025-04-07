document.addEventListener('DOMContentLoaded', function () {
    var sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            var sortForm = this.form; 
            if (sortForm) {
                sortForm.submit(); 
            } else {
                console.log("Error: Form not found.");
            }
        });
    }
});