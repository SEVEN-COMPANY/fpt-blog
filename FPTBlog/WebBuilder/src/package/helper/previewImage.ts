export function previewImage(inputId: string, previewId: string) {
    const imageInput = document.getElementById(inputId) as HTMLInputElement;
    imageInput.addEventListener('change', function () {
        const image = imageInput.files ? imageInput.files[0] : null;

        if (image) {
            const reader = new FileReader();

            reader.onload = function () {
                const dataURL = reader.result;
                const output = document.getElementById(previewId) as HTMLImageElement;
                if (output && dataURL && typeof dataURL == 'string') output.src = dataURL;
            };
            reader.readAsDataURL(image);
        }
    });
}
