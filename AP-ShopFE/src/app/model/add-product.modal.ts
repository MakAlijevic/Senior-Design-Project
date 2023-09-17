export class AddProduct {
    public productName: string;
    public imagePath: string;
    public price: number;
    public description: string;
    public quantity: number;
    public categoryId: number;
    public conditionId: number;
    public shippingId: number;

    constructor(productName: string, imagePath: string, price: number, description: string, quantity: number, categoryId: number, conditionId: number, shippingId: number) {
        this.productName = productName;
        this.imagePath = imagePath;
        this.price = price;
        this.description = description;
        this.quantity = quantity;
        this.categoryId = categoryId;
        this.conditionId = conditionId;
        this.shippingId = shippingId;
    }
}