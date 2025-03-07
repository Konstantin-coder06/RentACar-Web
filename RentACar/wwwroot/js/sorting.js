document.addEventListener('DOMContentLoaded', function () {
    var sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', function () {
            var sortForm = this.form; // Access the form containing the select
            if (sortForm) {
                sortForm.submit(); // Submit the form
            } else {
                console.log("Error: Form not found.");
            }
        });
    }
});