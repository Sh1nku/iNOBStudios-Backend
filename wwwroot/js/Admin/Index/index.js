$(document).ready(function () {
    new Vue({
        el: '#app',
        store: store,
        computed: {
            posts() {
                return this.$store.state.posts;
            },
            createPostVisible() {
                return this.$store.state.createPostVisible;
            }
        },
        methods: {
            toggleCreatePostVisible: function () {
                this.$store.commit('toggleCreatePostVisible');
            },
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init')
            });
        }
    });
});