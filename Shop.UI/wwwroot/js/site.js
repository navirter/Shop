var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        categories: [],
        selectedCategory: "All",
        products: []
    },
    mounted() {
        //this.getProducts();
        this.getCategories();
        this.selectCategory("All");
    },//not sure if this runs at page loading as i expect it to
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
        },
        getCategories: function () {
            this.loading = true;
            axios.get('/categories')
                .then(res => {
                    console.log(res);
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        selectCategory: function (category) {
            this.selectedCategory = category;
            this.loading = true;
            axios.get('/categories/' + category)
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