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