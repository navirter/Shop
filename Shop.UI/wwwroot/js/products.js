var app = new Vue({
    el: '#app',
    data: { //this whole data becomes LOWER CASE
        loading: false,
        categories: [],//string[]
        selectedCategory: "",
        products: null,
        /*
            public string Name { get; set; } //LOWER CASE
            public string Description { get; set; } //LOWER CASE
            public string Value { get; set; } //LOWER CASE
            public string Category { get; set; } //LOWER CASE
            public int StockCount { get; set; } //LOWER CASE
        */
        selectedProduct: null
    },
    beforeMount() {
        this.getCategories();
        this.selectCategory("All");
    },//Called right before the mounting begins: the render function is about to be called for the first time.
    mounted() {
    },//Called after the instance has just been mounted where el is replaced by the newly created vm.$el.
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
            axios.get('/products/categories')
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
            axios.get('/products/categories/' + category)
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
        selectProduct: function ()
        {

        }
    },
    computed: {
        productsCount: function () {
            return this.products.count;
        }
    }
});