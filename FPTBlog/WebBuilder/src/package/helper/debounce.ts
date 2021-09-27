let timeoutId: number;
export function debounce(time: number, callback: Function) {
    return (function () {
        if (timeoutId !== null) clearTimeout(timeoutId);

        timeoutId = setTimeout(callback, time);
    })();
}
