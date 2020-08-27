Vue.component('create-post-modal', {
    computed: {
        moveTaskModal() {
            return this.$store.state.moveTaskModal;
        }
    },
    props: {
        errors: Array
    },
    methods: {
        hide: function () {
            this.$store.commit('toggleCreatePostVisible');
        },
        submit: function () {
            this.$store.dispatch('createPost');
        }
    },
    template:
        `
<div class="backgroundStyle" @mousedown.self="hide">
    <div class="modalStyle">
        <span class="closeStyle" @click="hide">&times;</span>
        <h1>Create Post</h1>
        <p>Create a new post</p>
        <div v-if="errors">
            <ul v-for="error in errors">
                <li style="color: red">{{error}}</li>
            </ul>
        </div>
        <button type="button" class="btn btn-primary" @click="hide">Cancel</button>
    </div>
</div>
`

});