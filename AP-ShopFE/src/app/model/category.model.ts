export class Category {
    public id: number;
    public categoryName: string;
    public parentId: number;

    constructor(id: number, categoryName: string, parentId: number) {
        this.id = id;
        this.categoryName = categoryName;
        this.parentId = parentId;
    }
}