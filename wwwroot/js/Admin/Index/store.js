var store = new Vuex.Store({
    strict: true,
    state: {
        posts: null,
        createPostVisible: false
    },
    mutations: {
        setCreatePostVisible: (state, value) => {
            state.createPostVisible = value;
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
        }
    }
});