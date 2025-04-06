document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("calendarModal");
    const openBtn = document.getElementById("openCalendar");
    const closeBtn = document.querySelector(".close");
    const confirmBtn = document.getElementById("confirmDates");

    const startDateInput = document.getElementById("startDateInput");
    const endDateInput = document.getElementById("endDateInput");
    const form = document.getElementById("dateForm");

    const start = startDate !== null ? new Date(startDate) : null;
    const end = endDate !== null ? new Date(endDate) : null;

    // Validate dates
    const isValidStart = start && !isNaN(start.getTime());
    const isValidEnd = end && !isNaN(end.getTime());

    // Fallback dates
    const today = new Date();
    const threeDaysLater = new Date(today);
    threeDaysLater.setDate(today.getDate() + 3);

    // Set default dates
    let defaultDates;
    if (isValidStart && isValidEnd && start <= end) {
        defaultDates = [start, end];
    } else {
        defaultDates = [today, threeDaysLater];
    }

    // Initialize Flatpickr with validated dates
    const datePicker = flatpickr("#datePicker", {
        mode: "range",
        dateFormat: "Y-m-d",
        minDate: "today",
        maxDate: new Date().fp_incr(210),
        inline: true,
        numberOfMonths: 3,
        defaultDate: defaultDates // This now contains valid Date objects
    });


    // Close calendar modal
    closeBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });





    // Confirm date selection and submit
    confirmBtn.addEventListener("click", function () {
        let selectedDates = datePicker.selectedDates;
        console.log("Selected Dates:", selectedDates); // Debug
        if (selectedDates && selectedDates.length === 2) {
            let start = selectedDates[0].toISOString().split("T")[0];
            let end = selectedDates[1].toISOString().split("T")[0];
            startDateInput.value = start;
            endDateInput.value = end;

            modal.style.display = "none";
            form.submit();
        } else {
            alert("Please select both start and end dates.");
        }
    });

    // Search functionality
    const searchInput = document.getElementById("searchInput");

    searchInput.addEventListener("input", function () {
        const searchTerm = this.value.toLowerCase();
        const carItems = document.querySelectorAll(".car-item");

        carItems.forEach(item => {
            const title = item.getAttribute("data-title").toLowerCase();
            if (title.includes(searchTerm)) {
                item.style.display = "block";
            } else {
                item.style.display = "none";
            }
        });
    });


    // You can implement a dropdown or modal for sorting options here
    // Sort and Filter buttons (placeholders)
    document.getElementById("sortButton").addEventListener("click", function () {
        alert("Sort options would appear here");
    });


    document.getElementById("filtersButton").addEventListener("click", function () {
        alert("Filter options would appear here");
    });
});