var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        categories: [],//string[]
        selectedCategory: "All",
        products: []
        /*
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public string Category { get; set; }
            public int StockCount { get; set; }
        */
    },
    beforeMount() {        
        this.getCategories();
        this.selectCategory("All");
    },//not sure if this runs at page loading as i expect it to
    mounted() {

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
        }
    },
    computed: {

    }
});