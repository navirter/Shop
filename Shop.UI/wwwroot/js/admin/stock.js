var app = new Vue({
    el: "#app",
    data: {
        products: [],
        selectedProduct: null,
        newStock: {
            productId: 0, 
            description: "Size",
            qty: 10
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock: function () {
            this.loading = true;
            axios.get('/Admin/stocks')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        updateStock() {

        },
        addStock() {
            this.loading = true;
            this.newStock.productId = this.selectedProduct.id;
            axios.post('/Admin/stocks', this.newStock)
                .then(res => {
                    console.log(res);
                    this.selectProduct.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        selectProduct: function (product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        }
    }
});