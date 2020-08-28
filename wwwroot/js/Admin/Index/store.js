var store = new Vuex.Store({
    strict: true,
    state: {
        posts: null,
        createPostVisible: false,
        createPostErrors: []
    },
    mutations: {
        setCreatePostVisible: (state, value) => {
            state.createPostVisible = value;
        },
        setCreatePostErrors: (state, value) => {
            state.createPostErrors = value;
        },
        toggleCreatePostVisible: (state) => {
            state.createPostVisible = !state.createPostVisible;
        },
        setPosts: (state, value) => {
            state.posts = value;
        }
    },
    actions: {
        init: (context) => {
            context.commit('setPosts', model);
        },
        createPost: (context, payload) => {
            context.commit('setCreatePostErrors', []);
            $.ajax({
                type: "post",
                url: "/api/Post",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('setCreatePostVisible', false);
                },
                error: function (result) {
                    context.commit('setCreatePostErrors', parseErrors(result));
                }
            });
        }
    }
});