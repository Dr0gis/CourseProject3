package team.incorpore.dr0gis.elqueue.users;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;
import team.incorpore.dr0gis.elqueue.server.users.UsersResult;

/**
 * Created by dr0gi on 19.12.2017.
 */

public class QueueUserList extends AppCompatActivity {

    private List<UsersResult> usersResultList = new ArrayList<>(Arrays.asList(
        new UsersResult("dr0gis@mail.ru", "2017-12-19T4:25:45:119"),
        new UsersResult("exammple@mail.ru", "2017-12-19T4:25:45:119")
    ));

    private RecyclerView recyclerViewUsers;
    private UsersAdapter usersAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_queue_users);

        recyclerViewUsers = findViewById(R.id.rvUsers);
        recyclerViewUsers.setLayoutManager(new LinearLayoutManager(QueueUserList.this, LinearLayoutManager.VERTICAL, false));
        usersAdapter = new UsersAdapter(usersResultList);
        recyclerViewUsers.setAdapter(usersAdapter);
    }
}
