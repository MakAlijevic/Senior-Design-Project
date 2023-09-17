export class CartItem {
    public userId: number;
    public productId: number;
    public quantity: number;

    constructor(userId: number, productId: number, quantity: number) {
        this.userId = userId;
        this.productId = productId;
        this.quantity = quantity;
    }
}