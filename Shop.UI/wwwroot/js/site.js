var app = new Vue({
    el: '#app',
    data: {
        menu: 0,
        editing: false,
        loading: false,
        objectIndex: 0,
        productModel: {
            id: 0,
            name: "Product Name",
            description: "Product Description",
            value: 1.99,
            category: ""
        },
        products: []
    },
    mounted() {
        this.getProducts();

    },
    methods: {
        getProducts: function () {
            this.loading = true;
            axios.get('/products')
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
        }
    },
    computed: {

    }
});