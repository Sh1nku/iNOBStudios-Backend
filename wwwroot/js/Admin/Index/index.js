$(document).ready(function () {
    new Vue({
        el: '#app',
        store: store,
        computed: {
            posts() {
                return _.orderBy(this.$store.state.posts, 'addedTime', 'desc');
            },
            createPostVisible() {
                return this.$store.state.createPostVisible;
            },
            editPostVisible() {
                return this.$store.state.editPostVisible;
            }
        },
        methods: {
            toggleCreatePostVisible: function () {
                this.$store.commit('toggleCreatePostVisible');
            },
            toggleEditPostVisible: function (postId) {
                this.$store.commit('setCurrentPost', postId);
                this.$store.commit('toggleEditPostVisible');
            },
        },
        mounted: function () {
            this.$nextTick(function () {
                this.$store.dispatch('init');
            });
        }
    });
});