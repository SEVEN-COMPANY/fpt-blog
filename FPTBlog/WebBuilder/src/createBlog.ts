// import Quill from 'quill';

// @ts-nocheck

window.onload = function () {
    var editor = new Quill('#editor', {
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike'], // toggled buttons
                ['blockquote', 'code-block'],
                ['link', 'video', 'image'],
                [{ header: 1 }, { header: 2 }], // custom button values
                [{ list: 'ordered' }, { list: 'bullet' }],
                [{ script: 'sub' }, { script: 'super' }], // superscript/subscript
                [{ indent: '-1' }, { indent: '+1' }], // outdent/indent
                [{ direction: 'rtl' }], // text direction

                [{ size: ['small', false, 'large', 'huge'] }], // custom dropdown
                [{ header: [1, 2, 3, 4, 5, 6, false] }],
                // [({ color: [] } ,{ background: [] })], // dropdown with defaults from theme
                [{ font: [] }],
                [{ align: [] }],

                ['clean'], // remove formatting button
            ],
        },
        theme: 'snow',
    });

    function selectLocalImage() {
        const input = document.createElement('input');
        input.setAttribute('type', 'file');
        input.click();

        // Listen upload local image and save to server
        input.onchange = () => {
            const file = input.files[0];

            // file type is only image.
            if (/^image\//.test(file.type)) {
                saveToServer(file);
            } else {
                console.warn('You could only upload images.');
            }
        };
    }

    /**
     * Step2. save to server
     *
     * @param {File} file
     */
    function saveToServer(file: File) {
        const fd = new FormData();

        fd.append('image', file);
        insertToEditor('https://picsum.photos/200/300');

        // const xhr = new XMLHttpRequest();
        // xhr.open('POST', 'http://localhost:3000/upload/image', true);
        // xhr.onload = () => {
        //     if (xhr.status === 200) {
        //         // this is callback data: url
        //         const url = JSON.parse(xhr.responseText).data;
    }
    // };
    // xhr.send(fd);

    /**
     * Step3. insert image url to rich editor.
     *
     * @param {string} url
     */
    function insertToEditor(url: string) {
        // push image url to rich editor.
        const range = editor.getSelection();
        editor.insertEmbed(range.index, 'image', `${url}`);
    }

    editor.getModule('toolbar').addHandler('image', () => {
        selectLocalImage();
    });
};
