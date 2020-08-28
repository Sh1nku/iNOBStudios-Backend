Vue.component('create-post-modal', {
    computed: {
        moveTaskModal() {
            return this.$store.state.moveTaskModal;
        },
        errors() {
            return this.$store.state.createPostErrors;
        }
    },
    data() {
        return {
            title: ""
        }
    },
    methods: {
        hide: function () {
            this.$store.commit('toggleCreatePostVisible');
        },
        submit: function () {
            let payload = { title: this.title };
            this.$store.dispatch('createPost', payload);
        }
    },
    template:
        `
<div class="backgroundStyle" @mousedown.self="hide">
    <div class="modalStyle">
        <span class="closeStyle" @click="hide">&times;</span>
        <h1>Create Post</h1>
        <p>Create a new post</p>
        <table class="table">
            <tr>
                <th>Name</th>
                <td><input type='text' v-model="title"/></td>
            </tr>
        </table>
        <div v-if="errors">
            <ul v-for="error in errors">
                <li style="color: red">{{error}}</li>
            </ul>
        </div>
        <button type="button" class="btn btn-primary" @click="submit">Submit</button>
        <button type="button" class="btn btn-primary" @click="hide">Cancel</button>
    </div>
</div>
`

});