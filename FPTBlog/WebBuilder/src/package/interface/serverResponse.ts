export interface ServerResponse<T> {
    data: T;
    details: {
        [key: string]: string;
    };
}
