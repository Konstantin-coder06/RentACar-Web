document.addEventListener("DOMContentLoaded", function () {
    // Get all filter buttons within filter-chips
    const filterButtons = document.querySelectorAll('.filter-chip .filter-button');

    // Object to store active filters
    const activeFilters = {};

    // Add click event listener to each filter button
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Get the category from the button text
            const category = this.textContent.trim();

        
            this.classList.toggle('active');
            if (this.classList.contains('active')) {
                activeFilters[category] = true;
            } else {
                delete activeFilters[category];
            }

          
            filterCars();
        });
    });

  
    function filterCars() {
        const carItems = document.querySelectorAll('.car-item');
        const activeFilterCount = Object.keys(activeFilters).length;

      
        if (activeFilterCount === 0) {
            carItems.forEach(item => {
                item.style.display = 'block';
            });
            return;
        }

       
        carItems.forEach(item => {
          

          
            const carCategories = item.getAttribute('data-categories') || '';
            let shouldDisplay = false;

            Object.keys(activeFilters).forEach(filter => {
               
                const cleanFilter = filter.replace(/^[^\s]+ /, '');

                if (carCategories.includes(cleanFilter)) {
                    shouldDisplay = true;
                }
            });

            
            item.style.display = shouldDisplay ? 'block' : 'none';
        });
    }
});