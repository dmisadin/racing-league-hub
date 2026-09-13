export interface DropdownOption {
    id: number | string;
    name: string;
}

export interface PagedResult<T> {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
    hasMore: boolean;
}

export interface ProblemDetails {
    status: number;
    title: string;
    instance: string;
}