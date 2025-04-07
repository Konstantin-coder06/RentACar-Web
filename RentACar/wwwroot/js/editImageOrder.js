document.addEventListener("DOMContentLoaded", function () {
	const imagePreviewContainer = document.getElementById("imagePreviewContainer");
	const imageOrderInput = document.getElementById("imageOrder");
	const saveOrderButton = document.getElementById("saveOrderButton");


	new Sortable(imagePreviewContainer, {
		animation: 150,
		ghostClass: "sortable-ghost",
		onEnd: function () {
			updateImageOrder();
		}
	});


	function updateImageOrder() {
		const imageIds = [...imagePreviewContainer.children].map(item => item.getAttribute("data-id"));
		imageOrderInput.value = imageIds.join(",");
	}

	saveOrderButton.addEventListener("click", function () {
		const newOrder = imageOrderInput.value;

		fetch("/Car/UpdateImageOrder", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({ orderedImageIds: newOrder.split(",") })
		})
			.then(response => response.json())
			.then(data => {
				if (data.success) {
					alert("Image order updated successfully!");
				} else {
					alert("Error updating image order.");
				}
			});
	});
});